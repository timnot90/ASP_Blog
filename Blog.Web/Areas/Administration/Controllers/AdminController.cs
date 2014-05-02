using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Services;

namespace Blog.Web.Areas.Administration.Controllers
{
    public class AdminController : Controller
    {
        IBlogService _service = new BlogService();

        public ActionResult Index()
        {
            AdminModel model = new AdminModel();
            model.Users = _service.GetAllUserProfiles();
            return View(model);
        }

        public ActionResult ChangeRole()
        {
            return null;
        }
    }
}
