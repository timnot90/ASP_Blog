using System.Collections.Generic;
using Blog.Web.Models.AccountModel;
using Blog.Web.Models.Blog;

namespace Blog.Web.Services
{
    public interface IBlogService
    {
        void StoreBlogentry(AddBlogentryModel entry);
        List<BlogEntryListItemModel> GetAllBlogentries();
        BlogEntryListItemModel GetBlogentry(int id);
        void StoreUserProfile(UserProfileModel userProfileModel);
        List<UserProfileModel> GetAllUserProfiles();
        UserProfileModel GetUserProfile(int id);
    }
}