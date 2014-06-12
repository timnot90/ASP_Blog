using Blog.Core.Repositories;
using Blog.Web.Models.Shared;

namespace Blog.Web.Services.Shared
{
    public class BlogSharedService : IBlogSharedService
    {
        #region variables
        private readonly IBlogRepository _repository = new BlogRepository();
        #endregion

        #region General

        public FooterModel GetFooterModel()
        {
            return new FooterModel { FooterText = _repository.GetBlogSettings().FooterText };
        }

        public PageHeaderModel GetPageHeaderModel(string title)
        {
            return new PageHeaderModel { SiteName = _repository.GetBlogSettings().SiteName, Title = title };

        }

        public NavigationBarModel GetNavigationBarModel()
        {
            return new NavigationBarModel { SiteName = _repository.GetBlogSettings().SiteName };
        }

        public KeywordsModel GetKeywordsModel()
        {
            return new KeywordsModel(_repository.GetBlogSettings());
        }

        #endregion
    }
}