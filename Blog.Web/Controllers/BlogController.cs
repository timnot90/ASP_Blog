using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Models.Blog;
using Blog.Web.Services;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _service = new BlogService();

        public ActionResult Index()
        {
            return View(_service.GetAllBlogentries());
        }

        [HttpGet]
        public ActionResult AddEntry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEntry(AddBlogentryModel blogentry)
        {
            if (ModelState.IsValid)
            {
                _service.StoreBlogentry(blogentry);
            }
            return View(blogentry);
        }
    }
}
