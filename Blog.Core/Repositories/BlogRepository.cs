using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;

namespace Blog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        #region Blogentry
        public int SaveBlogentry(Blogentry entry, bool isNewEntry = false)
        {
            if (isNewEntry)
            {
                entry.CreationDate = DateTime.Now;
                BlogDataContext.Current.Blogentries.Add(entry);
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
            if (isNewEntry)
            {
                category.CreationDate = DateTime.Now;
                BlogDataContext.Current.Categories.Add(category);
            }
            BlogDataContext.Current.SaveChanges();
            return category.ID;
        }

        public void DeleteCategory(int categoryid)
        {
            BlogDataContext.Current.Categories.Remove(
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
            if (isNewProfile)
            {
                BlogDataContext.Current.UserProfiles.Add(userProfile);
            }
            BlogDataContext.Current.SaveChanges();
            return userProfile.ID;
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
