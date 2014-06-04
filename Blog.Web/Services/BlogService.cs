using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Account;
using Blog.Web.Models.Home;
using WebMatrix.WebData;

namespace Blog.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository = new BlogRepository();

        #region Blogentry

        public int StoreBlogentry( AddBlogentryModel entryModel )
        {
            bool isNewEntry = entryModel.Id == 0;
            Blogentry entry = isNewEntry ? new Blogentry() : _repository.GetBlogentry( entryModel.Id );
            entryModel.UpdateSource( entry );
            entry.CreatorID = WebSecurity.CurrentUserId;
            foreach (CategoryModel categoryModel in entryModel.Categories)
            {
                if (categoryModel.IsSelected)
                {
                    entry.Categories.Add( _repository.GetCategory( categoryModel.Id ) );
                }
            }
            return _repository.SaveBlogentry( entry, isNewEntry );
        }

        public List<BlogEntryListItemModel> GetAllBlogentries()
        {
            bool isFirst = true;
            return _repository.GetAllBlogentries().OrderByDescending( b => b.CreationDate ).Select(
                b =>
                {
                    BlogEntryListItemModel entryModel = new BlogEntryListItemModel( b );
                    if (!isFirst)
                    {
                        entryModel.Body = ShortenText( entryModel.Body );
                    }
                    isFirst = false;
                    return entryModel;
                } ).ToList();
        }

        public BlogentryDetailModel GetBlogentry( int id )
        {
            Blogentry entry = _repository.GetBlogentry( id );
            BlogentryDetailModel entryModel = entry == null ? null : new BlogentryDetailModel( entry );
            return entryModel;
        }

        public List<BlogEntryListItemModel> GetBlogentries( int categoryId, string monthAndYear )
        {
            int month = 0;
            int year = 0;
            bool monthAndYearIsValid = true;
            try
            {
                month = Convert.ToInt32( monthAndYear.Substring( 0, 2 ) );
                year = Convert.ToInt32( monthAndYear.Substring( 2, 4 ) );
                monthAndYearIsValid = monthAndYear.Length == 6;
            }
            catch (Exception)
            {
                monthAndYearIsValid = false;
            }

            List<BlogEntryListItemModel> blogentries = GetAllBlogentries().FindAll( e =>
            {
                bool categoryFits = categoryId == 0 || e.Categories.Any( c => c.Id == categoryId );
                bool monthAndYearFits = !monthAndYearIsValid ||
                                        (e.CreationDate.Month == month && e.CreationDate.Year == year);
                return categoryFits && monthAndYearFits;
            } );
            return blogentries;
        }

        #endregion

        #region Category

        public int StoreCategory( CategoryModel categoryModel )
        {
            bool isNewEntry = categoryModel.Id == 0;
            Category category = isNewEntry ? new Category() : _repository.GetCategory( categoryModel.Id );
            categoryModel.UpdateSource( category );
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            return _repository.SaveCategory( category, isNewEntry );
        }

        public void DeleteCategory( int categoryid )
        {
            _repository.DeleteCategory( categoryid );
        }

        public List<CategoryModel> GetAllCategories()
        {
            return _repository.GetAllCategories().OrderBy( c => c.Name ).Select(
                c =>
                {
                    CategoryModel categoryModel = new CategoryModel( c );
                    return categoryModel;
                } ).ToList();
        }

        public CategoryModel GetCategory( int id )
        {
            Category category = _repository.GetCategory( id );
            CategoryModel categoryModel = category == null ? null : new CategoryModel( category );
            return categoryModel;
        }

        #endregion

        #region UserProfile

        public int StoreUserProfile( UserProfileModel userProfileModel )
        {
            bool isNewProfile = userProfileModel.Id == 0;
            UserProfile newProfile = isNewProfile
                ? new UserProfile()
                : _repository.GetUserProfile( userProfileModel.Id );
            userProfileModel.UpdateSource( newProfile );
            return _repository.SaveUserProfile( newProfile, isNewProfile );
        }

        public int RegisterUser( RegisterModel registerModel )
        {
            if (_repository.EmailExists( registerModel.Email ))
            {
                throw new EmailAlreadyExistsException();
            }
            if (_repository.DisplayNameExists( registerModel.DisplayName ))
            {
                throw new DisplayNameAlreadyExistsException();
            }
            string token = WebSecurity.CreateUserAndAccount( registerModel.UserName, registerModel.Password,
                new
                {
                    UserNameLowercase = registerModel.UserName.ToLower(),
                    DisplayName = registerModel.DisplayName,
                    EMail = registerModel.Email,
                    EmailLowercase = registerModel.Email.ToLower(),
                }, true );

            MailSender.SendToken( token, /*registerModel.Email*/"timo.sit.blog@gmail.com" );

            UserProfile newProfile = new UserProfile();
            registerModel.UpdateSource( newProfile );
            return _repository.SaveUserProfile( newProfile, true );
        }

        public List<UserProfileModel> GetAllUserProfiles()
        {
            List<UserProfile> userProfiles = _repository.GetAllUserProfiles();
            List<UserProfileModel> userProfileModels = userProfiles.Select( e => new UserProfileModel( e ) ).ToList();
            return userProfileModels;
        }

        public UserProfileModel GetUserProfile( int id )
        {
            UserProfile userProfile = _repository.GetUserProfile( id );
            UserProfileModel userProfileModel = userProfile == null ? null : new UserProfileModel( userProfile );
            return userProfileModel;
        }

        public bool ValidateUser( string token )
        {
            try
            {
                return WebSecurity.ConfirmAccount( token );
            }
            catch (InvalidOperationException)
            {
                return false;
            }
//            string userName = ""; 
//            Roles.AddUserToRole( userName, CustomRoles.User );
        }

        #endregion

        #region Comment

        public int StoreComment( LeaveCommentModel commentModel )
        {
            bool isNewComment = commentModel.Id == 0;
            Comment comment = isNewComment ? new Comment() : _repository.GetComment( commentModel.Id );
            commentModel.UpdateSource( comment );
            comment.CreatorID = WebSecurity.CurrentUserId;
            comment.CreationDate = DateTime.Now;
            return _repository.SaveComment( comment, isNewComment );
        }

        public void DeleteComment( int commentId )
        {
            _repository.DeleteComment( commentId );
        }

        public CommentModel GetComment( int id )
        {
            Comment comment = _repository.GetComment( id );
            CommentModel commentModel = comment == null ? null : new CommentModel( comment );
            return commentModel;
        }

        #endregion

        #region Settings

        public BlogSettingsModel GetBlogSettings()
        {
            var blogSettings = new BlogSettingsModel(_repository.GetBlogSettings());
            return blogSettings;
        }
        #endregion

        #region private methods

        private static string ShortenText( string text )
        {
            return text.Length <= 500 ? text : text.Substring( 0, 500 ) + " ..";
        }

        #endregion
    }
}