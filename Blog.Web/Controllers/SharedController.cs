using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Services;
using Blog.Web.Services.Shared;

namespace Blog.Web.Controllers
{
    public class SharedController : Controller
    {
        #region variables
        private readonly IBlogSharedService _service = new BlogSharedService();
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult _PageHeader(string title)
        {
            return PartialView(_service.GetPageHeaderModel(title));
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult _NavigationBar()
        {
            return PartialView(_service.GetNavigationBarModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult _Footer()
        {
            return PartialView(_service.GetFooterModel());
        }
    }
}
