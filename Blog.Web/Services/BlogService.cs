using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.Repositories;
using Blog.Web.Models.Blog;
using WebMatrix.WebData;

namespace Blog.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository = new BlogRepository();
        #region Blogentry

        public void StoreBlogentry(AddBlogentryModel entryModel)
        {
            bool isNewEntry = entryModel.ID == 0;
            Blogentry entry = isNewEntry ? new Blogentry() : _repository.GetBlogentry(entryModel.ID);
            entryModel.UpdateSource(entry);
            entry.CreationDate = DateTime.Now;
            entry.CreatorID = WebSecurity.CurrentUserId;
            _repository.SaveBlogentry(entry, isNewEntry);
        }

        public List<BlogEntryListItemModel> GetAllBlogentries()
        {
            return _repository.GetAllBlogentries().OrderBy(b => b.CreationDate).Select(
                b =>
                {
                    BlogEntryListItemModel entryModel = new BlogEntryListItemModel(b);
                    entryModel.Creator = new UserProfileModel(_repository.GetUserProfile(b.CreatorID));

                    return entryModel;
                }).ToList();
        }

        public BlogEntryListItemModel GetBlogentry(int id)
        {
            Blogentry entry = _repository.GetBlogentry(id);
            BlogEntryListItemModel entryModel = entry == null ? null : new BlogEntryListItemModel(entry);
            if (entryModel != null)
            {
                entryModel.Creator = new UserProfileModel(_repository.GetUserProfile(entry.CreatorID));
            }
            return entryModel;
        }
        #endregion

        /*#region Category
        public void SaveCategory(Category category, bool isNewEntry = false);

        public List<Category> GetAllCategories();

        public Category GetCategory(int id);
        #endregion*/

        #region UserProfile
        public void StoreUserProfile(UserProfileModel userProfileModel)
        {
            bool isNewProfile = userProfileModel.ID == 0;
            UserProfile newProfile = isNewProfile ? new UserProfile() : _repository.GetUserProfile(userProfileModel.ID);
            userProfileModel.UpdateSource(newProfile);
            _repository.SaveUserProfile(newProfile, isNewProfile);
        }

        public List<UserProfileModel> GetAllUserProfiles()
        {
            List<UserProfile> userProfiles = _repository.GetAllUserProfiles();
            List<UserProfileModel> userProfileModels = userProfiles.Select(e => new UserProfileModel(e)).ToList();
            return userProfileModels;
        }

        public UserProfileModel GetUserProfile(int id)
        {
            UserProfile userProfile = _repository.GetUserProfile(id);
            UserProfileModel userProfileModel = userProfile == null ? null : new UserProfileModel(userProfile);
            return userProfileModel;
        }
        #endregion

    }
}