using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Models.Home;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace Blog.Web.Services.Home
{
    public class BlogHomeService : IBlogHomeService
    {
        #region variables

//        private static readonly List<string> Months = new List<string>(new[]
//        {
//            "01",
//            "02",
//            "03",
//            "04",
//            "05",
//            "06",
//            "07",
//            "08",
//            "09",
//            "10",
//            "11",
//            "12"
//        });

        private static readonly Dictionary<int, string> Months
            = new Dictionary<int, string>
            {
                {1, "Jan"},
                {2, "Feb"},
                {3, "Mar"},
                {4, "Apr"},
                {5, "May"},
                {6, "Jun"},
                {7, "Jul"},
                {8, "Aug"},
                {9, "Sep"},
                {10, "Okt"},
                {11, "Nov"},
                {12, "Dec"}
            };

        private readonly IBlogRepository _repository = new BlogRepository();

        #endregion

        #region Blogentry

        public int CreateNewBlogentry(AddBlogentryModel entryModel)
        {
            Blogentry entry = new Blogentry();
            entry.Header = FilterHtmlTags(entryModel.Header);
            entry.Body = FilterHtmlTags(entryModel.Body);

            entry.CreatorID = WebSecurity.CurrentUserId;
            entry.Categories = entryModel.Categories
                .Where(categoryModel => categoryModel.IsSelected)
                .Select( categoryModel => _repository.GetCategoryById(categoryModel.Id) ).ToList();
            return _repository.SaveBlogentry(entry, true);
        }

        public int SaveBlogentryChanges(EditBlogentryModel model)
        {
            Blogentry entry = _repository.GetBlogentry(model.Id);
            model.UpdateSource(entry);
            if (model.Categories != null)
            {
                entry.Categories.Clear();
                foreach (
                    CategoryModel categoryModel in model.Categories.Where(categoryModel => categoryModel.IsSelected))
                {
                    entry.Categories.Add(_repository.GetCategoryById(categoryModel.Id));
                }
            }
            entry.Header = FilterHtmlTags(entry.Header);
            entry.Body = FilterHtmlTags(entry.Body);
            return _repository.SaveBlogentry(entry);
        }

        public void DeleteBlogentry(int id)
        {
            _repository.DeleteBlogentry(id);
        }

        public BlogentryListModel GetBlogentryListModel()
        {
            bool isFirst = true;
            BlogentryListModel model = new BlogentryListModel
            {
                Blogentries = _repository.GetAllBlogentries().OrderByDescending(b => b.CreationDate).Select(
                    b =>
                    {
                        BlogentryListItemModel entryModel = new BlogentryListItemModel(b);
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
            if (entryModel != null)
            {
                entryModel.CommentsActivated = _repository.GetBlogSettings().CommentsActivated;
            }
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
            BlogentryListModel model = GetBlogentryListModel();

            model.Blogentries = _repository.GetAllBlogentries().Where(e =>
            {
                bool categoryFits = categoryId == 0 || e.Categories.Any(c => c.ID == categoryId);
                bool monthAndYearFits = !monthAndYearIsValid ||
                                        (e.CreationDate.Month == month && e.CreationDate.Year == year);
                return categoryFits && monthAndYearFits;
            }).Select(e =>
            {
                BlogentryListItemModel listItemModel = new BlogentryListItemModel(e);
                listItemModel.Body = ShortenText(e.Body);
                return listItemModel;
            }).ToList();

            return model;
        }

        public BlogSidebarModel GetBlogSidebarModel()
        {
            List<Blogentry> allBlogentries = _repository.GetAllBlogentries();
            BlogSidebarModel model = new BlogSidebarModel
            {
                AvailableYears = allBlogentries
                    .GroupBy(m => m.CreationDate.Year)
                    .Select(m => m.Key.ToString(CultureInfo.InvariantCulture))
                    .ToList(),
                AvailableMonths = allBlogentries
                    .GroupBy(m => m.CreationDate.Month).ToDictionary( m => m.Key.ToString("d2"), m => Months[m.Key]),
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public AddBlogentryModel GetAddBlogentryModel()
        {
            AddBlogentryModel model = new AddBlogentryModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public EditBlogentryModel GetEditBlogentryModel(int id)
        {
            Blogentry entry = _repository.GetBlogentry(id);
            EditBlogentryModel model = entry == null ? null : new EditBlogentryModel(entry);
            if (model != null)
            {
                model.Categories = _repository.GetAllCategories().Select(c => new CategoryModel(c)).ToList();
                foreach (CategoryModel category in
                    model.Categories.Where(category => entry.Categories.Any(c => c.ID == category.Id)))
                {
                    category.IsSelected = true;
                }
                model.Body = ReplaceBrWithNewlines(model.Body);
            }
            return model;
        }

        #endregion

        #region Category

        public int CreateCategory(CategoryModel categoryModel)
        {
            if (_repository.GetCategoryByName(categoryModel.Name) != null)
            {
                throw new CategoryAlreadyExistsException();
            }
            Category category = new Category();
            categoryModel.UpdateSource(category);
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            category.UserProfile = _repository.GetUserProfileById( category.CreatorID );
            return _repository.SaveCategory(category, true);
        }

        public void DeleteCategory(int categoryid)
        {
            _repository.DeleteCategory(categoryid);
        }

        public CategoryListModel GetCategoryListModel()
        {
            CategoryListModel model = new CategoryListModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public CategoryModel GetCategory(int id)
        {
            Category category = _repository.GetCategoryById(id);
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

        public int CreateComment(LeaveCommentModel commentModel)
        {
            Comment comment = new Comment();
            commentModel.UpdateSource(comment);
            comment.CreatorID = WebSecurity.CurrentUserId == -1 ? (int?) null : WebSecurity.CurrentUserId;
            comment.CreationDate = DateTime.Now;
            comment.Header = FilterHtmlTags(comment.Header);
            comment.Body = FilterHtmlTags(comment.Body);
            comment.Blogentry = _repository.GetBlogentry( commentModel.BlogentryId );
            return _repository.SaveComment(comment, true);
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

        private static string ReplaceBrWithNewlines(string text)
        {
            Regex replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            return replaceBrWithNewline.Replace(text, "\r\n");
        }
        private string FilterHtmlTags(string text)
        {
            if (text == null) return null;

            Regex replaceBrWithNewline = new Regex( @"<br[\s]*/?>" );
            Regex removeHtml = new Regex( @"<[^>]*>" );
            Regex replaceNewlineWithBr = new Regex( @"(\r\n)|\r|\n" );
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace( replaceBrWithNewline.Replace( text, "\r\n" ), "" ), "<br/>" );
        }
        #endregion
    }
}