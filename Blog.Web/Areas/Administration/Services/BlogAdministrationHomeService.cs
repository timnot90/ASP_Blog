using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Shared;
using WebMatrix.WebData;

namespace Blog.Web.Areas.Administration.Services
{
    public class BlogAdministrationHomeService : IBlogAdministrationHomeService
    {
        #region variables

        private readonly IBlogRepository _repository = new BlogRepository();

        #endregion

        public AddBlogentryModel GetAddBlogentryModel()
        {
            AddBlogentryModel model = new AddBlogentryModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public int CreateNewBlogentry(AddBlogentryModel entryModel)
        {
            Blogentry entry = new Blogentry();
            entry.Header = FilterHtmlTags(entryModel.Header);
            entry.Body = FilterHtmlTags(entryModel.Body);

            entry.CreatorID = WebSecurity.CurrentUserId;
            entry.Categories = entryModel.Categories
                .Where(categoryModel => categoryModel.IsSelected)
                .Select(categoryModel => _repository.GetCategoryById(categoryModel.Id)).ToList();
            return _repository.SaveBlogentry(entry, true);
        }

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

        #region Categories
        public int CreateCategory(AddCategoryModel categoryModel)
        {
            if (_repository.GetCategoryByName(categoryModel.Name) != null)
            {
                throw new CategoryAlreadyExistsException();
            }
            Category category = new Category();
            categoryModel.UpdateSource(category);
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            category.UserProfile = _repository.GetUserProfileById(category.CreatorID);
            return _repository.SaveCategory(category, true);
        }

        public void DeleteCategory(int categoryid)
        {
            _repository.DeleteCategory(categoryid);
        }

        public CategoryListModel GetCategoryListModel()
        {
            CategoryListModel model = new CategoryListModel
            {
                Categories = GetAllCategoryModels()
            };
            return model;
        }

        public CategoryModel GetCategory(int id)
        {
            Category category = _repository.GetCategoryById(id);
            CategoryModel categoryModel = category == null ? null : new CategoryModel(category);
            return categoryModel;
        }

        private List<CategoryModel> GetAllCategoryModels()
        {
            return _repository.GetAllCategories().OrderBy(c => c.Name).Select(
                c => new CategoryModel(c)).ToList();
        }

        #endregion
        
        private string FilterHtmlTags(string text)
        {
            if (text == null) return null;

            Regex replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            Regex removeHtml = new Regex(@"<[^>]*>");
            Regex replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }
    }
}