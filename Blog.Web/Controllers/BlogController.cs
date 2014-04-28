using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Blog.Web.Models.Blog;
using Blog.Web.Services;
using Recaptcha;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        IBlogService _service = new BlogService();

        /// <summary>
        /// Shows all blogentries.
        /// </summary>
        /// <param name="categoryId">the id of the category to show blogentries for</param>
        /// <param name="monthAndYear">a string containing numbers for month and year (mmyyyy)</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int categoryId, string monthAndYear = "")
        {
            List<BlogEntryListItemModel> blogentries = _service.GetAllBlogentries().FindAll(b =>
            {
                return categoryId == 0 || b.Categories.Any(c => c.Id == categoryId);
            });
            return View(blogentries);
        }

        [HttpGet]
        public ActionResult AddBlogentry()
        {
            AddBlogentryModel model = new AddBlogentryModel();
            model.Categories = _service.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBlogentry(AddBlogentryModel blogentry)
        {
            if (ModelState.IsValid)
            {
                int id = _service.StoreBlogentry(blogentry);
                return RedirectToAction("ShowBlogentry", new {id });
            }
            return View(blogentry);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowBlogentry(int id)
        {
            BlogentryDetailModel model = _service.GetBlogentry(id);
            if (model != null)
            {
                return View(model);
            }
            return View("BlogentryNotFound", id);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Categories()
        {
            CategoryListModel categoryListModel = new CategoryListModel();
            categoryListModel.Categories = _service.GetAllCategories();
            return View(categoryListModel);
        }

        [HttpGet]
        public PartialViewResult AddCategory()
        {
            return PartialView(new CategoryModel());
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _service.StoreCategory(model);
                return RedirectToAction("Categories");
            }
            //TODO: add and show error when model state is not valid
            return RedirectToAction("Categories", model);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryid)
        {
            _service.DeleteCategory(categoryid);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public PartialViewResult _AddBlogentryAvailableCategories()
        {
            return PartialView(_service.GetAllCategories());
        }

        [HttpPost]
        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        public ActionResult _LeaveComment(LeaveCommentModel comment, bool captchaValid, string captchaErrorMessage)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("captcha", captchaErrorMessage);
            }
            if (ModelState.IsValid)
            {
                _service.StoreComment(comment);
                return RedirectToAction("ShowBlogentry", new {id = comment.BlogentryId});
            }
            else
            {
                return View();
            }
        }
    }
}
