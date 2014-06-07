using Blog.Web.Areas.Administration.Models;

namespace Blog.Web.Areas.Administration.Services
{
    public interface IBlogAdministrationService
    {
        UserListModel GetUserListModel();
        BlogSettingsModel GetBlogSettings();
        void StoreSettings(BlogSettingsModel model);
    }
}