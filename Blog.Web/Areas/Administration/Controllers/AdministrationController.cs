using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Services;

namespace Blog.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = CustomRoles.Administrator)]
    public class AdministrationController : Controller
    {
        IBlogService _service = new BlogService();

        public ActionResult Users()
        {
            UsersModel model = new UsersModel();
            model.Users = _service.GetAllUserProfiles();
            return View(model);
        }

        public ActionResult ChangeRole(string username, string newRole, bool added)
        {
            if (added)
            {
                Roles.AddUserToRole(username, newRole);
            }
            else
            {
                Roles.RemoveUserFromRole(username, newRole);
            }
            return null;
        }

        public ActionResult _UserListItem()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BlogSettings()
        {
            return View(_service.GetBlogSettings());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogSettings(BlogSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _service.StoreSettings( model );
                model.SuccessfullySaved = true;
                return View(model);
            }
            model.SuccessfullySaved = false;
            return View(model);
        }
    }
}
