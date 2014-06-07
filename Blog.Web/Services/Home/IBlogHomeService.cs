using Blog.Web.Models.Home;

namespace Blog.Web.Services.Home
{
    public interface IBlogHomeService
    {
        int StoreBlogentry(AddBlogentryModel entryModel);
        BlogentryListModel GetBlogentryListModel();
        BlogentryDetailModel GetBlogentry(int id);
        BlogentryListModel GetBlogentryListModel(int categoryId, string monthAndYear);
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