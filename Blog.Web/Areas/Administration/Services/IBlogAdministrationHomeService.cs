using Blog.Web.Areas.Administration.Models.Home;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Services
{
    public interface IBlogAdministrationHomeService
    {
        AddBlogentryModel GetAddBlogentryModel();
        int CreateNewBlogentry(AddBlogentryModel entryModel);
        EditBlogentryModel GetEditBlogentryModel(int id);
        // ReSharper disable once UnusedMethodReturnValue.Global
        int SaveBlogentryChanges(EditBlogentryModel model);
        UserListModel GetUserListModel();
        void ChangeRole( int id, string newRole, bool added );
        void SetUserLockedState( int userId, bool state );
        BlogSettingsModel GetBlogSettings();
        void StoreSettings(BlogSettingsModel model);
        CategoryListModel GetCategoryListModel();
        void DeleteCategory( int categoryid );
        // ReSharper disable once UnusedMethodReturnValue.Global
        int CreateCategory( AddCategoryModel categoryModel );
    }
}