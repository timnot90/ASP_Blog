using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Blog.Web.Models.Blog;
using Blog.Web.Services;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _service = new BlogService();

        /// <summary>
        /// Shows all blogentries.
        /// </summary>
        /// <param name="categoryId">the id of the category to show blogentries for</param>
        /// <param name="monthAndYear">a string containing numbers for month and year (mmyyyy)</param>
        /// <returns></returns>
        public ActionResult Index(int categoryId, string monthAndYear)
        {
            List<BlogEntryListItemModel> blogentries = _service.GetAllBlogentries().FindAll(b =>
            {
                return categoryId == 0 ||  b.Categories.FindAll(c => c.ID == categoryId).Count > 0;
            });
            return View(blogentries);
        }

        [HttpGet]
        public ActionResult AddBlogentry()
        {
            AddBlogentryModel model = new AddBlogentryModel();
            model.AvailableCategories = _service.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBlogentry(AddBlogentryModel blogentry)
        {
            if (ModelState.IsValid)
            {
                _service.StoreBlogentry(blogentry);
            }
            return View(blogentry);
        }

        public ActionResult ShowBlogentry(int id)
        {
            return View(_service.GetBlogentry(id));
        }

        public ActionResult Categories()
        {
            CategoryListModel categoryListModel = new CategoryListModel();
            categoryListModel.Categories = _service.GetAllCategories();
            return View(categoryListModel);
        }

        public PartialViewResult AddCategory()
        {
            return PartialView(new CategoryModel());
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel model)
        {
            _service.StoreCategory(model);
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int categoryid)
        {
            _service.DeleteCategory(categoryid);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult _AddBlogentryAvailableCategories()
        {
            return PartialView(_service.GetAllCategories());
        }
    }
}
