using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Home;
using CategoryModel = Blog.Web.Models.Home.CategoryModel;

namespace Blog.Web.Services.Home
{
    public interface IBlogHomeService
    {
        #region Blogentry
        int SaveBlogentryChanges(EditBlogentryModel model);
        void DeleteBlogentry(int id);
        BlogentryListModel GetBlogentryListModel();
        BlogentryDetailModel GetBlogentry(int id);
        EditBlogentryModel GetEditBlogentryModel( int id );
        BlogentryListModel GetBlogentryListModel(int categoryId, string monthAndYear );
        #endregion

        #region Comment
        int CreateComment(LeaveCommentModel commentModel);
        void DeleteComment(int commentId);
        CommentModel GetComment(int id);
        #endregion
        BlogSidebarModel GetBlogSidebarModel();
    }
}