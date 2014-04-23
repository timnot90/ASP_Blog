using System.Collections.Generic;
using Blog.Web.Models.Account;
using Blog.Web.Models.Blog;

namespace Blog.Web.Services
{
    public interface IBlogService
    {
        void StoreBlogentry(AddBlogentryModel entry);
        List<BlogEntryListItemModel> GetAllBlogentries();
        BlogEntryListItemModel GetBlogentry(int id);

        void StoreCategory(CategoryModel categoryModel);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);

        void StoreUserProfile(UserProfileModel userProfileModel);
        List<UserProfileModel> GetAllUserProfiles();
        UserProfileModel GetUserProfile(int id);
        void DeleteCategory(int categoryid);
    }
}