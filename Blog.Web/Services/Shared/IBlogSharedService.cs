using Blog.Web.Models.Shared;

namespace Blog.Web.Services.Shared
{
    public interface IBlogSharedService
    {
        FooterModel GetFooterModel();
        PageHeaderModel GetPageHeaderModel(string title);
        NavigationBarModel GetNavigationBarModel();
        KeywordsModel GetKeywordsModel();
        BlogSettingsModel GetBlogSettings();
    }
}