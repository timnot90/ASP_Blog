using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Account;

namespace Blog.Web.Areas.Administration.Services
{
    public class BlogAdministrationService : IBlogAdministrationService
    {
        #region variables
        private readonly IBlogRepository _repository = new BlogRepository();
        #endregion

        public UserListModel GetUserListModel()
        {
            var model = new UserListModel
            {
                Users = _repository.GetAllUserProfiles().Select(e => new UserProfileModel(e)).ToList()
            };
            return model;
        }


        #region Settings

        public BlogSettingsModel GetBlogSettings()
        {
            return  new BlogSettingsModel(_repository.GetBlogSettings());
        }

        public void StoreSettings(BlogSettingsModel model)
        {
            Setting setting = _repository.GetBlogSettings();
            model.UpdateSource(setting);
            _repository.StoreSettings(setting);
        }

        #endregion
    }
}