using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Services
{
    public class BlogAdministrationService : IBlogAdministrationService
    {
        #region variables

        private readonly IBlogRepository _repository = new BlogRepository();

        #endregion

        #region Users

        public UserListModel GetUserListModel()
        {
            UserListModel model = new UserListModel
            {
                Users = _repository.GetAllUserProfiles().Select( e => new UserListItemModel( e ) ).ToList()
            };

            foreach (UserListItemModel user in model.Users)
            {
                user.Roles =
                    Roles.GetAllRoles()
                        .Select( r => new RoleModel( r, Roles.GetRolesForUser( user.UserName ).Contains( r ) ) )
                        .ToList();
            }
            return model;
        }

        public void SetUserLockedState( int userId, bool state )
        {
            _repository.SetUserLockedSate( userId, state );
        }

        public void ChangeRole( int id, string newRole, bool added )
        {
            try
            {
                string username = _repository.GetUserProfileById( id ).UserName;
                if (added)
                {
                    Roles.AddUserToRole( username, newRole );
                }
                else
                {
                    Roles.RemoveUserFromRole( username, newRole );
                }
            }
            catch (ArgumentNullException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (ProviderException)
            {
            }
        }

        #endregion

        #region Blogsettings

        public BlogSettingsModel GetBlogSettings()
        {
            return new BlogSettingsModel( _repository.GetBlogSettings() );
        }

        public void StoreSettings( BlogSettingsModel model )
        {
                Setting setting = _repository.GetBlogSettings();
                model.UpdateSource( setting );
                _repository.StoreSettings( setting );
        }
        #endregion
    }
}