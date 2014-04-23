using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Blog.Core.DataAccess.Blog;

namespace Blog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        #region Blogentry
        public int SaveBlogentry(Blogentry entry, bool isNewEntry = false)
        {
            int id = 0;
            if (isNewEntry)
            {
                entry.CreationDate = DateTime.Now;
                id = BlogDataContext.Current.Blogentries.Add(entry).ID;
            }
            BlogDataContext.Current.SaveChanges();
            return entry.ID;
        }

        public List<Blogentry> GetAllBlogentries()
        {
            return BlogDataContext.Current.Blogentries.OrderBy(e => e.CreationDate).ToList();
        }

        public Blogentry GetBlogentry(int id)
        {
            return BlogDataContext.Current.Blogentries.FirstOrDefault(e => e.ID == id);
        }
        #endregion

        #region Category
        public int SaveCategory(Category category, bool isNewEntry = false)
        {
            int id = 0;
            if (isNewEntry)
            {
                category.CreationDate = DateTime.Now;
                id = BlogDataContext.Current.Categories.Add(category).ID;
            }
            BlogDataContext.Current.SaveChanges();
            return id;
        }

        public void DeleteCategory(int categoryid)
        {
            Category category = BlogDataContext.Current.Categories.Remove(
                BlogDataContext.Current.Categories.FirstOrDefault(c => c.ID == categoryid));
            BlogDataContext.Current.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return BlogDataContext.Current.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return BlogDataContext.Current.Categories.FirstOrDefault(c => c.ID == id);
        }
        #endregion

        #region UserProfile
        public int SaveUserProfile(UserProfile userProfile, bool isNewProfile = false)
        {
            int id = 0;
            if (isNewProfile)
            {
                id = BlogDataContext.Current.UserProfiles.Add(userProfile).ID;
            }
            BlogDataContext.Current.SaveChanges();
            return id;
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            return BlogDataContext.Current.UserProfiles.OrderBy(u => u.UserName).ToList();
        }

        public UserProfile GetUserProfile(int id)
        {
            return BlogDataContext.Current.UserProfiles.FirstOrDefault(u => u.ID == id);
        }
        #endregion
    }
}
