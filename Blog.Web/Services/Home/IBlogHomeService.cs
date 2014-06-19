using Blog.Web.Models.Home;

namespace Blog.Web.Services.Home
{
    public interface IBlogHomeService
    {
        #region Blogentry
        AddBlogentryModel GetAddBlogentryModel();
        int CreateNewBlogentry(AddBlogentryModel entryModel);
        int SaveBlogentryChanges(EditBlogentryModel model);
        void DeleteBlogentry(int id);
        BlogentryListModel GetBlogentryListModel();
        BlogentryDetailModel GetBlogentry(int id);
        EditBlogentryModel GetEditBlogentryModel( int id );
        BlogentryListModel GetBlogentryListModel(int categoryId, string monthAndYear );
        #endregion

        #region Category
        int CreateCategory(CategoryModel categoryModel);
        void DeleteCategory(int categoryid);
        CategoryListModel GetCategoryListModel();
        CategoryModel GetCategory(int id);
        #endregion

        #region Comment
        int StoreComment(LeaveCommentModel commentModel);
        void DeleteComment(int commentId);
        CommentModel GetComment(int id);
        #endregion
        BlogSidebarModel GetBlogSidebarModel();
    }
}