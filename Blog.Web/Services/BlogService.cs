using Blog.Core.DataAccess.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Repositories;
using Blog.Web.Models.Account;
using Blog.Web.Models.Home;
using WebMatrix.WebData;

namespace Blog.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository = new BlogRepository();
        #region Blogentry

        public int StoreBlogentry(AddBlogentryModel entryModel)
        {
            bool isNewEntry = entryModel.Id == 0;
            Blogentry entry = isNewEntry ? new Blogentry() : _repository.GetBlogentry(entryModel.Id);
            entryModel.UpdateSource(entry);
            entry.CreatorID = WebSecurity.CurrentUserId;
            foreach (CategoryModel categoryModel in entryModel.Categories)
            {
                if (categoryModel.IsSelected)
                {
                    entry.Categories.Add(_repository.GetCategory(categoryModel.Id));
                }
            }
            return _repository.SaveBlogentry(entry, isNewEntry);
        }

        public List<BlogEntryListItemModel> GetAllBlogentries()
        {
            return _repository.GetAllBlogentries().OrderByDescending(b => b.CreationDate).Select(
                b =>
                {
                    BlogEntryListItemModel entryModel = new BlogEntryListItemModel(b);
                    return entryModel;
                }).ToList();
        }

        public BlogentryDetailModel GetBlogentry(int id)
        {
            Blogentry entry = _repository.GetBlogentry(id);
            BlogentryDetailModel entryModel = entry == null ? null : new BlogentryDetailModel(entry);
            return entryModel;
        }
        #endregion

        #region Category
        public int StoreCategory(CategoryModel categoryModel)
        {
            bool isNewEntry = categoryModel.Id == 0;
            Category category = isNewEntry ? new Category() : _repository.GetCategory(categoryModel.Id);
            categoryModel.UpdateSource(category);
            category.CreationDate = DateTime.Now;
            category.CreatorID = WebSecurity.CurrentUserId;
            return _repository.SaveCategory(category, isNewEntry: isNewEntry);
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
                    return categoryModel;
                }).ToList();
        }

        public CategoryModel GetCategory(int id)
        {
            Category category = _repository.GetCategory(id);
            CategoryModel categoryModel = category == null ? null : new CategoryModel(category);
            return categoryModel;
        }
        #endregion

        #region UserProfile
        public int StoreUserProfile(UserProfileModel userProfileModel)
        {
            bool isNewProfile = userProfileModel.Id == 0;
            UserProfile newProfile = isNewProfile ? new UserProfile() : _repository.GetUserProfile(userProfileModel.Id);
            userProfileModel.UpdateSource(newProfile);
            return _repository.SaveUserProfile(newProfile, isNewProfile: isNewProfile);
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

        public int StoreComment(LeaveCommentModel commentModel)
        {
            bool isNewComment = commentModel.Id == 0;
            Comment comment = isNewComment ? new Comment() : _repository.GetComment(commentModel.Id);
            commentModel.UpdateSource(comment);
            comment.CreatorID = WebSecurity.CurrentUserId;
            comment.CreationDate = DateTime.Now;
            return _repository.SaveComment(comment, isNewComment);
        }

        public CommentModel GetComment(int id)
        {
            Comment comment = _repository.GetComment(id);
            CommentModel commentModel = comment == null ? null : new CommentModel(comment);
            return commentModel;
        }
    }
}