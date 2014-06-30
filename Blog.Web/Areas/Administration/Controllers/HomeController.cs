using System.Web.Mvc;
using Blog.Web.Areas.Administration.Services;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        readonly IBlogAdministrationHomeService _service = new BlogAdministrationHomeService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View(_service.GetUserListModel());
        }

        public void ChangeRole(int id, string newRole, bool added)
        {
            _service.ChangeRole(id, newRole, added);
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
        public ActionResult BlogSettings(BlogSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _service.StoreSettings(model);
                model.SuccessfullySaved = true;
                return View(model);
            }
            model.SuccessfullySaved = false;
            return View(model);
        }

        public void SetUserLockedState(int id, bool state)
        {
            _service.SetUserLockedState(id, state);
        }

    }
}
