using Blog.Web.Models.Home;

namespace Blog.Web.Services.Home
{
    public interface IBlogHomeService
    {
        int CreateNewBlogentry(AddBlogentryModel entryModel);
        int SaveBlogentryChanges(EditBlogentryModel model);
        BlogentryListModel GetBlogentryListModel();
        BlogentryDetailModel GetBlogentry(int id);
        EditBlogentryModel GetEditBlogentryModel( int id );
        BlogentryListModel GetBlogentryListModel(int categoryId, string monthAndYear, string searchText );
        BlogSidebarModel GetBlogSidebarModel();
        AddBlogentryModel GetAddBlogentryModel();
        int StoreCategory(CategoryModel categoryModel);
        void DeleteCategory(int categoryid);
        CategoryListModel GetCategoryListModel();
        CategoryModel GetCategory(int id);
        int StoreComment(LeaveCommentModel commentModel);
        void DeleteComment(int commentId);
        CommentModel GetComment(int id);
    }
}