using System.Web.Mvc;
using Blog.Web.Models.Shared;
using Blog.Web.Services.Shared;

namespace Blog.Web.Controllers
{
    public class SharedController : Controller
    {
        #region variables

        private readonly IBlogSharedService _service = new BlogSharedService();

        #endregion

        [AllowAnonymous]
        public PartialViewResult _PageHeader( string title )
        {
            return PartialView( _service.GetPageHeaderModel( title ) );
        }

        [AllowAnonymous]
        public PartialViewResult _NavigationBar()
        {
            return PartialView( _service.GetNavigationBarModel() );
        }

        [AllowAnonymous]
        public PartialViewResult _Footer()
        {
            return PartialView( _service.GetFooterModel() );
        }

//        [HttpPost]
//        [AllowAnonymous]
//        public PartialViewResult _PageHeader( PageHeaderModel model )
//        {
//            return PartialView(_service.GetPageHeaderModel(model.Title));
//        }
//
//        [HttpPost]
//        [AllowAnonymous]
//        public PartialViewResult _NavigationBar(NavigationBarModel model)
//        {
//            return PartialView(_service.GetNavigationBarModel());
//        }
//
//        [HttpPost]
//        [AllowAnonymous]
//        public PartialViewResult _Footer(FooterModel model)
//        {
//            return PartialView(_service.GetFooterModel());
//        }

        [AllowAnonymous]
        public PartialViewResult _Keywords(NavigationBarModel model)
        {
            return PartialView(_service.GetKeywordsModel());
        }

//        [HttpPost]
//        [AllowAnonymous]
//        public PartialViewResult _Keywords(FooterModel model)
//        {
//            return PartialView(_service.GetKeywordsModel());
//        }
    }
}