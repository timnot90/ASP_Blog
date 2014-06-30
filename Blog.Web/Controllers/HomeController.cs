using System;
using System.IO;
using System.Web.Mvc;
using Blog.Core.Exceptions;
using Blog.Web.Models.Home;
using Blog.Web.Services.Home;
using Blog.Web.Services.Shared;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables

        private readonly IBlogHomeService _service = new BlogHomeService();
        private readonly IBlogSharedService _sharedService = new BlogSharedService();

        #endregion

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

        [AllowAnonymous]
        public ActionResult Blogentry( int id )
        {
            BlogentryDetailModel model = _service.GetBlogentry( id );
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
                if (ModelState.IsValid)
                {
                    int newCommentId = _service.CreateComment(model);
                    //return PartialView("_Comment", _service.GetComment(newCommentId));
                    return Json( new {success=true, data = RenderPartialViewToString( "_Comment", _service.GetComment( newCommentId ) )} );
                }
            }
            else
            {
                ModelState.AddModelError("CommentsDeactivated", "The comments are disabled by the administrator.");
            }
            //return PartialView(model);
            return Json(new { success = false, data = RenderPartialViewToString("_LeaveComment", model) });
        }

        [AllowAnonymous]
        public PartialViewResult _BlogSidebar()
        {
            return PartialView( _service.GetBlogSidebarModel() );
        }

        [HttpGet]
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult _EditBlogentry(int blogentryId)
        {
            return PartialView(_service.GetEditBlogentryModel( blogentryId ));
        }

        [HttpPost]
        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult _EditBlogentry(EditBlogentryModel model)
        {
            if (ModelState.IsValid)
            {
                _service.SaveBlogentryChanges( model );
//                return Json( new {html = Blogentry( model.Id ), statusCode = 200});
                return RedirectToAction( "Blogentry", "Home", new {area = "", id = model.Id} );
            }
//            return Json(new { html = "", statusCode = 403 });
            return PartialView( model );
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
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
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