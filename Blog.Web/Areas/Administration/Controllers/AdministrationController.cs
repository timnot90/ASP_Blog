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

        public ActionResult ChangeRole(string username, string newRole)
        {
            if (!Roles.GetRolesForUser(username).Contains(newRole))
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

        public ActionResult BlogSettings()
        {
            return View(_service.GetBlogSettings());
        }
    }
}
