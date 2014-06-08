﻿using Blog.Web.Areas.Administration.Models;

namespace Blog.Web.Areas.Administration.Services
{
    public interface IBlogAdministrationService
    {
        UserListModel GetUserListModel();
        void ChangeRole( int id, string newRole, bool added );
        void SetUserLockedState( int userId, bool state );
        BlogSettingsModel GetBlogSettings();
        void StoreSettings(BlogSettingsModel model);
    }
}