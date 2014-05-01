using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Home;
using Blog.Web.Services;
using Recaptcha;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IBlogService _service = new BlogService();

        public ActionResult Index(int? categoryId, string monthAndYear)
        {
            if (!categoryId.HasValue)
            {
                categoryId = 0;
            }
            return View(FilterEntries((int)categoryId, monthAndYear));
        }

        /*
        /// <summary>
        /// Shows all blogentries.
        /// </summary>
        /// <param name="categoryId">the id of the category to show blogentries for</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int categoryId)
        {
            return View(FilterEntries(categoryId, string.Empty));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthAndYear">ddyyyy</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string monthAndYear = "")
        {
            return View(FilterEntries(0, monthAndYear));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId">the id of the category to show blogentries for</param>
        /// <param name="monthAndYear">
        ///     a string containing month and year of the blogentries to show.
        ///     Format: mmyyyy
        ///     <example>032014 for March 2014</example>
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int categoryId, string monthAndYear)
        {
            return View(FilterEntries(categoryId, monthAndYear));
        }
        */

        private List<BlogEntryListItemModel> FilterEntries(int categoryId, string monthAndYear)
        {
            int month = 0;
            int year = 0;
            bool monthAndYearIsValid = true;
            try
            {
                month = Convert.ToInt32(monthAndYear.Substring(0, 2));
                year = Convert.ToInt32(monthAndYear.Substring(2, 4));
                monthAndYearIsValid = monthAndYear.Length == 6;
            }
            catch (InvalidCastException)
            {
                monthAndYear = string.Empty;
                monthAndYearIsValid = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                monthAndYear = string.Empty;
                monthAndYearIsValid = false;
            }
            catch (NullReferenceException)
            {
                monthAndYear = string.Empty;
                monthAndYearIsValid = false;
            }

            List<BlogEntryListItemModel> blogentries = _service.GetAllBlogentries().FindAll(e =>
            {
                bool categoryFits = categoryId == 0 || e.Categories.Any(c => c.Id == categoryId);
                bool monthAndYearFits = !monthAndYearIsValid || (e.CreationDate.Month == month && e.CreationDate.Year == year);
                return categoryFits && monthAndYearFits;
            });
            return blogentries;
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
                return RedirectToAction("ShowBlogentry", new { id });
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
            if (!WebSecurity.IsAuthenticated && !captchaValid)
            {
                ModelState.AddModelError("captcha", captchaErrorMessage);
            }
            if (ModelState.IsValid)
            {
                _service.StoreComment(comment);
                return RedirectToAction("ShowBlogentry", new { id = comment.BlogentryId });
            }
            return View();
        }

//        [HttpPost]
//        public ActionResult _LeaveCommentAuthenticated(LeaveCommentModel comment)
//        {
//            if (ModelState.IsValid)
//            {
//                _service.StoreComment(comment);
//                return RedirectToAction("ShowBlogentry", new { id = comment.BlogentryId });
//            }
//            return View("_LeaveComment");
//        }
    }
}
