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
            entry.CreatorID = WebSecurity.CurrentUserId;
            foreach (CategoryModel categoryModel in entryModel.AvailableCategories)
            {
                if (categoryModel.IsSelected)
                {
                    entry.Categories.Add(_repository.GetCategory(categoryModel.ID));
                }
            }
            _repository.SaveBlogentry(entry, isNewEntry);
        }

        public List<BlogEntryListItemModel> GetAllBlogentries()
        {
            return _repository.GetAllBlogentries().OrderByDescending(b => b.CreationDate).Select(
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

        #region Category
        public void StoreCategory(CategoryModel categoryModel)
        {
            bool isNewEntry = categoryModel.ID == 0;
            Category category = isNewEntry ? new Category() : _repository.GetCategory(categoryModel.ID);
            categoryModel.UpdateSource(category);
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            _repository.SaveCategory(category, isNewEntry);
        }

        public void DeleteCategory(int categoryid)
        {
            _repository.DeleteCategory(categoryid);
        }

        public List<CategoryModel> GetAllCategories()
        {
            return _repository.GetAllCategories().OrderBy(c => c.Name).Select(
                c =>
                {
                    CategoryModel categoryModel = new CategoryModel(c);
                    categoryModel.Creator = new UserProfileModel(_repository.GetUserProfile(c.CreatorID));
                    return categoryModel;
                }).ToList();
        }

        public CategoryModel GetCategory(int id)
        {
            Category category = _repository.GetCategory(id);
            CategoryModel categoryModel = category == null ? null : new CategoryModel(category);
            if (categoryModel != null)
            {
                categoryModel.Creator = new UserProfileModel(_repository.GetUserProfile(category.CreatorID));
            }
            return categoryModel;
        }
        #endregion

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