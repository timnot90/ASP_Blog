using System.Web.Mvc;
using Blog.Core.Exceptions;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Areas.Administration.Services;
using Blog.Web.Models.Home;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        readonly IBlogAdministrationHomeService _service = new BlogAdministrationHomeService();

        [HttpGet]
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult AddBlogentry()
        {
            return View(_service.GetAddBlogentryModel());
        }

        [HttpPost]
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult AddBlogentry(AddBlogentryModel blogentry)
        {
            if (ModelState.IsValid)
            {
                int id = _service.CreateNewBlogentry(blogentry);
                return RedirectToAction("Blogentry", new { area="", id });
            }
            return View(blogentry);
        }

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

        [AllowAnonymous]
        public ActionResult Categories()
        {
            return View(_service.GetCategoryListModel());
        }
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult DeleteCategory(int categoryid)
        {
            try
            {
                _service.DeleteCategory(categoryid);
            }
            catch (BlogDbException)
            {
                // BlogDbException is occuring here, when you try to delete a category
                // even though it is already deleted.
            }
            return RedirectToAction("Categories");
        }
        [HttpGet]
        [Authorize(Roles = CustomRoles.Administrator)]
        public PartialViewResult _AddCategory()
        {
            return PartialView();
        }
        [HttpPost]
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult AddCategory(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.CreateCategory(model);
                }
                catch (CategoryAlreadyExistsException)
                {
                    ModelState.AddModelError("CategoryAlreadyExists", "A category with the entered name already exists.");
                }
            }
            return View("Categories", _service.GetCategoryListModel());
        }




    }
}
