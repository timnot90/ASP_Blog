using System.Collections.Generic;
using Blog.Web.Models.Account;
using Blog.Web.Models.Blog;

namespace Blog.Web.Services
{
    public interface IBlogService
    {
        int StoreBlogentry(AddBlogentryModel entry);
        List<BlogEntryListItemModel> GetAllBlogentries();
        BlogentryDetailModel GetBlogentry(int id);

        int StoreCategory(CategoryModel categoryModel);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);

        int StoreUserProfile(UserProfileModel userProfileModel);
        List<UserProfileModel> GetAllUserProfiles();
        UserProfileModel GetUserProfile(int id);
        void DeleteCategory(int categoryid);
    }
}