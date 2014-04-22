using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.DataAccess.Blog;

namespace Blog.Core.Repositories
{
    public interface IBlogRepository
    {
        #region Blogentry
        void SaveBlogentry(Blogentry entry, bool isNewEntry = false);

        List<Blogentry> GetAllBlogentries();

        Blogentry GetBlogentry(int id);
        #endregion

        #region Category
        void SaveCategory(Category category, bool isNewEntry = false);

        List<Category> GetAllCategories();

        Category GetCategory(int id);
        #endregion

        #region UserProfile
        void SaveUserProfile(UserProfile userProfile, bool isNewProfile = false);

        List<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfile(int id);
        #endregion

        void DeleteCategory(int categoryid);
    }
}
