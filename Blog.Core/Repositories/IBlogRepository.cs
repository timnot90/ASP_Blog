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
        #endregion

        #region Category
        int SaveCategory(Category category, bool isNewEntry = false);

        List<Category> GetAllCategories();

        Category GetCategory(int id);
        #endregion

        #region UserProfile
        int SaveUserProfile(UserProfile userProfile, bool isNewProfile = false);

        List<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfile(int id);
        #endregion

        void DeleteCategory(int categoryid);
    }
}
