using Blog.Web.Models.Home;

namespace Blog.Web.Services.Home
{
    public interface IBlogHomeService
    {
        #region Blogentry
        void DeleteBlogentry(int id);
        BlogentryListModel GetBlogentryListModel();
        BlogentryDetailModel GetBlogentryDetailModel(int id);
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