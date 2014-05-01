using System.Collections.Generic;
using Blog.Web.Models.Account;
using Blog.Web.Models.Home;

namespace Blog.Web.Services
{
    public interface IBlogService
    {
        #region Blogentry
        int StoreBlogentry(AddBlogentryModel entry);
        List<BlogEntryListItemModel> GetAllBlogentries();
        BlogentryDetailModel GetBlogentry(int id);
        #endregion
        
        #region Category
        int StoreCategory(CategoryModel categoryModel);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        #endregion
        
        #region UserProfile
        int StoreUserProfile(UserProfileModel userProfileModel);
        List<UserProfileModel> GetAllUserProfiles();
        UserProfileModel GetUserProfile(int id);
        void DeleteCategory(int categoryid);
        #endregion

        #region Comment
        int StoreComment(LeaveCommentModel commentModel);
        CommentModel GetComment(int id);
        #endregion
    }
}