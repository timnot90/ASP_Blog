using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Blog.Core.DataAccess.Blog;

namespace Blog.Core.Repositories
{
    public interface IBlogRepository
    {
        #region Blogentry
        int SaveBlogentry(Blogentry entry, bool isNewEntry = false);

        List<Blogentry> GetAllBlogentries();

        Blogentry GetBlogentry(int id);
        #endregion

        #region Category
        int SaveCategory(Category category, bool isNewEntry = false);

        List<Category> GetAllCategories();

        Category GetCategory(int id);

        void DeleteCategory(int categoryid);
        #endregion

        #region UserProfile
        int SaveUserProfile(UserProfile userProfile, bool isNewProfile = false);

        List<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfile(int id);

        bool EmailExists(string email);

        bool DisplayNameExists(string displayName);

        UserProfile GetUserByRegistrationToken( string token );

        void SetUserLockedSate( int userId, bool state );
        #endregion

        #region Comment
        int SaveComment(Comment comment, bool isNewComment = false);

        void DeleteComment(int commentId);

        List<Comment> GetAllComments();

        Comment GetComment(int id);
        #endregion

        #region Settings

        Setting GetBlogSettings();
        void StoreSettings( Setting settings );

        #endregion
    }
}
