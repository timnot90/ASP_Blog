using System.Collections.Generic;
using Blog.Core.DataAccess.Blog;

namespace Blog.Core.Repositories
{
    public interface IBlogRepository
    {
        #region Blogentry
        int SaveBlogentry(Blogentry entry, bool isNewEntry = false);
        List<Blogentry> GetAllBlogentries();
        Blogentry GetBlogentry(int id);
        void DeleteBlogentry(int id);
        #endregion

        #region Category
        int SaveCategory(Category category, bool isNewEntry = false);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void DeleteCategory(int categoryid);
        Category GetCategoryByName(string name);
        #endregion

        #region UserProfile
        int SaveUserProfile(UserProfile userProfile, bool isNewProfile = false);

        IEnumerable<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfileById(int id);

        UserProfile GetUserProfileByUsername( string username );

        bool EmailExists(string email);

        bool DisplayNameExists(string displayName);

        UserProfile GetUserByRegistrationToken( string token );

        void SetUserLockedSate( int userId, bool state );
        #endregion

        #region Comment
        int SaveComment(Comment comment, bool isNewComment = false);

        void DeleteComment(int commentId);

        Comment GetComment(int id);
        #endregion

        #region Settings

        Setting GetBlogSettings();
        void StoreSettings( Setting settings );

        #endregion
    }
}
