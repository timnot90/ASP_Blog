using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Services
{
    public interface IBlogAdministrationHomeService
    {
        AddBlogentryModel GetAddBlogentryModel();
        int CreateNewBlogentry( AddBlogentryModel entryModel );
        UserListModel GetUserListModel();
        void ChangeRole( int id, string newRole, bool added );
        void SetUserLockedState( int userId, bool state );
        BlogSettingsModel GetBlogSettings();
        void StoreSettings(BlogSettingsModel model);
        CategoryModel GetCategory( int id );
        CategoryListModel GetCategoryListModel();
        void DeleteCategory( int categoryid );
        int CreateCategory( AddCategoryModel categoryModel );
    }
}