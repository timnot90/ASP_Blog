using System.Collections.Generic;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Account;
using Blog.Web.Models.Home;

namespace Blog.Web.Services
{
    public interface IBlogService
    {
        #region Blogentry
        int StoreBlogentry(AddBlogentryModel entry);
        List<BlogEntryListItemModel> GetAllBlogentries();
        List<BlogEntryListItemModel> GetBlogentries(int categoryId, string monthAndYear);
        BlogentryDetailModel GetBlogentry(int id);
        #endregion
        
        #region Category
        int StoreCategory(CategoryModel categoryModel);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        #endregion
        
        #region UserProfile
        int StoreUserProfile(UserProfileModel userProfileModel);
        int RegisterUser(RegisterModel registerModel);
        List<UserProfileModel> GetAllUserProfiles();
        UserProfileModel GetUserProfile(int id);
        void DeleteCategory(int categoryid);
        bool ValidateUser( string token );
        void SendPasswordResetToken(ResetPasswordModel model);
        void ResetPasswordSecondStep(ResetPasswordSecondStepModel model);
        #endregion

        #region Comment
        int StoreComment(LeaveCommentModel commentModel);
        void DeleteComment(int commentId);
        CommentModel GetComment(int id);
        #endregion

        #region Settings

        BlogSettingsModel GetBlogSettings();
        void StoreSettings( BlogSettingsModel model );

        #endregion
    }
}