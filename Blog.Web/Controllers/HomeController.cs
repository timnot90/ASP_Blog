using System;
using System.IO;
using System.Web.Mvc;
using Blog.Core.Exceptions;
using Blog.Core.Extensions;
using Blog.Web.Models.Home;
using Blog.Web.Services.Home;
using Blog.Web.Services.Shared;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables

        private readonly IBlogHomeService _service = new BlogHomeService();
        private readonly IBlogSharedService _sharedService = new BlogSharedService();

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index( int? categoryId, string monthAndYear)
        {
            if (!categoryId.HasValue)
            {
                categoryId = 0;
            }
            if (categoryId == 0 && string.IsNullOrEmpty( monthAndYear ))
            {
                return View( _service.GetBlogentryListModel() );
            }
            return View( _service.GetBlogentryListModel( (int) categoryId, monthAndYear) );
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Blogentry( int id )
        {
            BlogentryDetailModel model = _service.GetBlogentryDetailModel( id );
            if (model != null)
            {
                return View( model );
            }
            return View( "BlogentryNotFound", id );
        }

        [HttpPost]
//        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        [AllowAnonymous]
        public ActionResult _LeaveComment( LeaveCommentModel model/*, bool captchaValid, string captchaErrorMessage */)
        {
            if (_sharedService.GetBlogSettings().CommentsActivated)
            {
                if (!WebSecurity.IsAuthenticated & !CaptchaHelper.ValidateCaptchaResult(model.CaptchaResult))
                {
                    ModelState.AddModelError("InvalidCaptchaResult", "The entered value for the captcha is invalid.");
                }
                if (ModelState.IsValid)
                {
                    int newCommentId = _service.CreateComment(model);
                    return Json( new {success=true, data = RenderPartialViewToString( "_Comment", _service.GetComment( newCommentId ) )} );
                }
            }
            else
            {
                ModelState.AddModelError("CommentsDeactivated", "The comments are disabled by the administrator.");
            }
            return Json(new { success = false, data = RenderPartialViewToString("_LeaveComment", model) });
        }

        [AllowAnonymous]
        public PartialViewResult _BlogSidebar()
        {
            return PartialView( _service.GetBlogSidebarModel() );
        }

        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult DeleteComment( int commentId, int blogentryId )
        {
            try
            {
                _service.DeleteComment( commentId );
            }
            catch (BlogDbException)
            {
                // BlogDbException is occuring here, when you try to delete a comment
                // that is already deleted.
            }
            return RedirectToAction( "Blogentry", new {id = blogentryId} );
        }

        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult DeleteBlogentry(int id)
        {
            try
            {
                _service.DeleteBlogentry( id );
            }
            catch (BlogDbException)
            {
                // BlogDbException is occuring here, when you try to delete a blogentry
                // even though it is already deleted.
            }
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            try
            {
                using (var sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}