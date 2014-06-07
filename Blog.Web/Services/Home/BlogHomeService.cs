﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Blog.Web.Models.Home;
using WebMatrix.WebData;

namespace Blog.Web.Services.Home
{
    public class BlogHomeService : IBlogHomeService
    {
        #region variables
        private static readonly List<string> Months = new List<string>(new[]
        {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"
        });
        private readonly IBlogRepository _repository = new BlogRepository();
        #endregion

        #region Blogentry

        public int StoreBlogentry(AddBlogentryModel entryModel)
        {
            bool isNewEntry = entryModel.Id == 0;
            Blogentry entry = isNewEntry ? new Blogentry() : _repository.GetBlogentry(entryModel.Id);
            entryModel.UpdateSource(entry);
            entry.CreatorID = WebSecurity.CurrentUserId;
            foreach (CategoryModel categoryModel in entryModel.Categories)
            {
                if (categoryModel.IsSelected)
                {
                    entry.Categories.Add(_repository.GetCategory(categoryModel.Id));
                }
            }
            return _repository.SaveBlogentry(entry, isNewEntry);
        }

        public BlogentryListModel GetBlogentryListModel()
        {
            bool isFirst = true;
            BlogentryListModel model = new BlogentryListModel
            {
                Blogentries = _repository.GetAllBlogentries().OrderByDescending(b => b.CreationDate).Select(
                    b =>
                    {
                        BlogEntryListItemModel entryModel = new BlogEntryListItemModel(b);
                        if (!isFirst)
                        {
                            entryModel.Body = ShortenText(entryModel.Body);
                        }
                        isFirst = false;
                        return entryModel;
                    }).ToList(),
                NumberOfBlogentriesPerPage = _repository.GetBlogSettings().NumberOfEntriesPerPage
            };
            return model;
        }

        public BlogentryDetailModel GetBlogentry(int id)
        {
            Blogentry entry = _repository.GetBlogentry(id);
            BlogentryDetailModel entryModel = entry == null ? null : new BlogentryDetailModel(entry);
            return entryModel;
        }

        public BlogentryListModel GetBlogentryListModel(int categoryId, string monthAndYear)
        {
            int month = 0;
            int year = 0;
            bool monthAndYearIsValid;
            try
            {
                month = Convert.ToInt32(monthAndYear.Substring(0, 2));
                year = Convert.ToInt32(monthAndYear.Substring(2, 4));
                monthAndYearIsValid = monthAndYear.Length == 6;
            }
            catch (Exception)
            {
                monthAndYearIsValid = false;
            }
            List<BlogEntryListItemModel> blogentries = GetBlogentryListModel().Blogentries.FindAll(e =>
            {
                bool categoryFits = categoryId == 0 || e.Categories.Any(c => c.Id == categoryId);
                bool monthAndYearFits = !monthAndYearIsValid ||
                                        (e.CreationDate.Month == month && e.CreationDate.Year == year);
                return categoryFits && monthAndYearFits;
            });
            BlogentryListModel model = GetBlogentryListModel();
            model.Blogentries.RemoveAll(e =>
            {
                bool categoryFits = categoryId == 0 || e.Categories.Any(c => c.Id == categoryId);
                bool monthAndYearFits = !monthAndYearIsValid ||
                                        (e.CreationDate.Month == month && e.CreationDate.Year == year);
                return !(categoryFits && monthAndYearFits);
            });
            //new BlogentryListModel {Blogentries = blogentries};
            return model;
        }

        public BlogSidebarModel GetBlogSidebarModel()
        {
            var model = new BlogSidebarModel
            {
                AvailableYears = _repository.GetAllBlogentries()
                    .GroupBy(m => m.CreationDate.Year)
                    .Select(m => m.Key.ToString())
                    .ToList(),
                AvailableMonths = Months,
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public AddBlogentryModel GetAddBlogentryModel()
        {
            var model = new AddBlogentryModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        #endregion

        #region Category

        public int StoreCategory(CategoryModel categoryModel)
        {
            bool isNewEntry = categoryModel.Id == 0;
            Category category = isNewEntry ? new Category() : _repository.GetCategory(categoryModel.Id);
            categoryModel.UpdateSource(category);
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            return _repository.SaveCategory(category, isNewEntry);
        }

        public void DeleteCategory(int categoryid)
        {
            _repository.DeleteCategory(categoryid);
        }

        public CategoryListModel GetCategoryListModel()
        {
            var model = new CategoryListModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public CategoryModel GetCategory(int id)
        {
            Category category = _repository.GetCategory(id);
            CategoryModel categoryModel = category == null ? null : new CategoryModel(category);
            return categoryModel;
        }

        private List<CategoryModel> GetAllCategoryModels()
        {
            return _repository.GetAllCategories().OrderBy(c => c.Name).Select(
                c => new CategoryModel(c)).ToList();
        }

        #endregion

        #region Comment

        public int StoreComment(LeaveCommentModel commentModel)
        {
            bool isNewComment = commentModel.Id == 0;
            Comment comment = isNewComment ? new Comment() : _repository.GetComment(commentModel.Id);
            commentModel.UpdateSource(comment);
            comment.CreatorID = WebSecurity.CurrentUserId;
            comment.CreationDate = DateTime.Now;
            return _repository.SaveComment(comment, isNewComment);
        }

        public void DeleteComment(int commentId)
        {
            _repository.DeleteComment(commentId);
        }

        public CommentModel GetComment(int id)
        {
            Comment comment = _repository.GetComment(id);
            CommentModel commentModel = comment == null ? null : new CommentModel(comment);
            return commentModel;
        }

        #endregion

        #region private methods

        private static string ShortenText(string text)
        {
            return text.Length <= 500 ? text : text.Substring(0, 500) + " ..";
        }

        #endregion
    }
}