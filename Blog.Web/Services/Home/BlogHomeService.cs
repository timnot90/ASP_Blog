using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Home;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;
using CategoryModel = Blog.Web.Models.Home.CategoryModel;

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
                listItemModel.Body = ShortenText(e.BodyWithBr);
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
        #endregion

        #region Category
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
        #endregion
    }
}