using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models.Home;
using Blog.Web.Models.Shared;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace Blog.Web.Areas.Administration.Services
{
    public class BlogAdministrationHomeService : IBlogAdministrationHomeService
    {
        #region variables

        private readonly IBlogRepository _repository = new BlogRepository();

        #endregion

        public EditBlogentryModel GetEditBlogentryModel( int id )
        {
            Blogentry entry = _repository.GetBlogentry( id );
            EditBlogentryModel model = entry == null ? null : new EditBlogentryModel( entry );
            if (model != null)
            {
                model.Categories = _repository.GetAllCategories().Select( c => new CategorySelectedModel( c ) ).ToList();
                model.Categories.Where( category => entry.Categories.Any( c => c.ID == category.Id ) )
                    .ForEach( c => c.IsSelected = true );
            }
            return model;
        }

        public AddBlogentryModel GetAddBlogentryModel()
        {
            var model = new AddBlogentryModel
            {
                Categories = _repository.GetAllCategories().OrderBy( c => c.Name ).Select(
                c => new CategorySelectedModel( c ) ).ToList()
            };
            return model;
        }

        public int CreateNewBlogentry( AddBlogentryModel entryModel )
        {
            var entry = new Blogentry();
            entryModel.UpdateSource( entry );
            entry.CreatorID = WebSecurity.CurrentUserId;
            entry.Categories = entryModel.Categories
                .Where( categoryModel => categoryModel.IsSelected )
                .Select( categoryModel => _repository.GetCategoryById( categoryModel.Id ) ).ToList();
            return _repository.SaveBlogentry( entry, true );
        }

        public int SaveBlogentryChanges( EditBlogentryModel model )
        {
            Blogentry entry = _repository.GetBlogentry( model.Id );
            model.UpdateSource( entry );
            if (model.Categories != null)
            {
                entry.Categories.Clear();
                model.Categories.Where( c => c.IsSelected )
                    .ForEach( c => entry.Categories.Add( _repository.GetCategoryById( c.Id ) ) );
            }
            return _repository.SaveBlogentry( entry );
        }

        #region Users

        public UserListModel GetUserListModel()
        {
            var model = new UserListModel
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

        #region Categories

        public int CreateCategory( AddCategoryModel categoryModel )
        {
            if (_repository.GetCategoryByName( categoryModel.Name ) != null)
            {
                throw new CategoryAlreadyExistsException();
            }
            var category = new Category();
            categoryModel.UpdateSource( category );
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            category.UserProfile = _repository.GetUserProfileById( category.CreatorID );
            return _repository.SaveCategory( category, true );
        }

        public void DeleteCategory( int categoryid )
        {
            _repository.DeleteCategory( categoryid );
        }

        public CategoryListModel GetCategoryListModel()
        {
            var model = new CategoryListModel
            {
                Categories = _repository.GetAllCategories().OrderBy( c => c.Name ).Select(
                c => new CategoryListItemModel( c ) ).ToList()
            };
            return model;
        }

        #endregion
    }
}