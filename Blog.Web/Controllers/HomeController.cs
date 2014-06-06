using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Web.Models.Home;
using Blog.Web.Models.Shared;
using Blog.Web.Services;
using Recaptcha;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables
        private static readonly List<string> months = new List<string>(new[]
        {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"
        });
        private readonly IBlogService _service = new BlogService();
        #endregion

        #region views
        [AllowAnonymous]
        public ActionResult Index(int? categoryId, string monthAndYear)
        {
            BlogentryListModel model = new BlogentryListModel();
            model.NumberOfBlogentriesPerPage = _service.GetBlogSettings().NumberOfEntriesPerPage;
            
            if (!categoryId.HasValue)
            {
                categoryId = 0;
            }
            if (categoryId == 0 && string.IsNullOrEmpty(monthAndYear))
            {
                model.Blogentries = _service.GetAllBlogentries();
            }
            model.Blogentries = _service.GetBlogentries((int) categoryId, monthAndYear);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Categories()
        {
            CategoryListModel categoryListModel = new CategoryListModel();
            categoryListModel.Categories = _service.GetAllCategories();
            return View(categoryListModel);
        }

        [AllowAnonymous]
        public ActionResult Blogentry(int id)
        {
            BlogentryDetailModel model = _service.GetBlogentry(id);
            if (model != null)
            {
                return View(model);
            }
            return View("BlogentryNotFound", id);
        }

        [HttpGet]
        public ActionResult AddBlogentry()
        {
            AddBlogentryModel model = new AddBlogentryModel();
            model.Categories = _service.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlogentry(AddBlogentryModel blogentry)
        {
            if (ModelState.IsValid)
            {
                int id = _service.StoreBlogentry(blogentry);
                return RedirectToAction("Blogentry", new {id});
            }
            return View(blogentry);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryid)
        {
            _service.DeleteCategory(categoryid);
            return RedirectToAction("Categories");
        }
        #endregion

        #region partial views
        [HttpPost]
        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        public ActionResult _LeaveComment(LeaveCommentModel comment, bool captchaValid, string captchaErrorMessage)
        {
            if (!WebSecurity.IsAuthenticated && !captchaValid)
            {
                ModelState.AddModelError("captcha", captchaErrorMessage);
            }
            if (ModelState.IsValid)
            {
                _service.StoreComment(comment);
                return RedirectToAction("Blogentry", new {id = comment.BlogentryId});
            }
            return View();
        }

        public ActionResult _BlogSidebar()
        {
            BlogSidebarModel model = new BlogSidebarModel();
            model.Categories = _service.GetAllCategories();
            model.AvailableYears =
                _service.GetAllBlogentries()
                    .GroupBy(m => m.CreationDate.Year)
                    .Select(m => m.Key.ToString())
                    .ToList();
            model.AvailableMonths = months;

            return View(model);
        }

        [HttpPost]
        public ActionResult _BlogSidebar(BlogSidebarModel model)
        {
            return Index(model.Categories[0].Id, model.SelectedMonth + model.SelectedYear);
//            return RedirectToAction("Index",
//                new {categoryId = model.Categories[0], monthAndYear = model.SelectedMonth + model.SelectedYear});
        }

        [HttpGet]
        public PartialViewResult _Footer()
        {
            var model = new FooterModel {FooterText = _service.GetBlogSettings().FooterText};
            return PartialView( model );
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
        #endregion

        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                int blogentryId = _service.GetComment(commentId).BlogentryId;
                _service.DeleteComment(commentId);
                return RedirectToAction("Blogentry", new {id = blogentryId});
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorModel(ex.Message));
            }
        }

        [HttpGet]
        public PartialViewResult _PageHeader(string title)
        {
            var model = new PageHeaderModel {SiteName = _service.GetBlogSettings().SiteName};
            model.Title = title;
            return PartialView(model);
        }

        public PartialViewResult _NavigationBar()
        {
            var model = new NavigationBarModel {SiteName = _service.GetBlogSettings().SiteName};
            return PartialView( model );
        }
    }
}