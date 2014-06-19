using System.Web.Mvc;
using Blog.Core.Exceptions;
using Blog.Core.Extensions;
using Blog.Web.Models.Home;
using Blog.Web.Services.Home;
using Blog.Web.Services.Shared;
using Recaptcha;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables

        private readonly IBlogHomeService _service = new BlogHomeService();
        private readonly IBlogSharedService _sharedService = new BlogSharedService();

        #endregion

        #region views

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
        public ActionResult Categories()
        {
            return View( _service.GetCategoryListModel() );
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

        [HttpGet]
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult AddBlogentry()
        {
            return View( _service.GetAddBlogentryModel() );
        }

        [HttpPost]
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult AddBlogentry( AddBlogentryModel blogentry )
        {
            if (ModelState.IsValid)
            {
                int id = _service.CreateNewBlogentry( blogentry );
                return RedirectToAction( "Blogentry", new {id} );
            }
            return View( blogentry );
        }

        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult DeleteCategory( int categoryid )
        {
            _service.DeleteCategory( categoryid );
            return RedirectToAction( "Categories" );
        }

        #endregion

        #region partial views

        [HttpPost]
//        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        [AllowAnonymous]
        public ActionResult _LeaveComment( LeaveCommentModel comment/*, bool captchaValid, string captchaErrorMessage */)
        {
            if (_sharedService.GetBlogSettings().CommentsActivated)
            {
                bool captchaValid = CaptchaHelper.ValidateCaptchaResult(comment.CaptchaResult);
                if (!WebSecurity.IsAuthenticated && !captchaValid)
                {
                    ModelState.AddModelError("captcha", "Your input for the captcha result was wrong.");
                }
                if (ModelState.IsValid)
                {
                    int newCommentId = _service.StoreComment(comment);
                    return PartialView("_Comment", _service.GetComment(newCommentId));
                }
            }
            else
            {
                ModelState.AddModelError("CommentsDeactivated", "The comments are disabled by the administrator.");
            }
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult _BlogSidebar()
        {
            return PartialView( _service.GetBlogSidebarModel() );
        }

        [HttpGet]
        [Authorize( Roles = CustomRoles.Administrator )]
        public PartialViewResult AddCategory()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult AddCategoryPost( CategoryModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int newCategoryId = _service.CreateCategory(model);
                    model.Id = newCategoryId;
//                    return View("Categories", _service.GetCategoryListModel());
//                    return PartialView("_Category", model);
                }
                catch (CategoryAlreadyExistsException)
                {
                    ModelState.AddModelError("CategoryAlreadyExists", "A category with the entered name already exists.");
                }
            }
            return View("Categories", _service.GetCategoryListModel());
        }

        [HttpGet]
        public ActionResult _EditBlogentry(int blogentryId)
        {
            return PartialView(_service.GetEditBlogentryModel( blogentryId ));
        }

        [HttpPost]
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
        #endregion

        [HttpGet] // I would rather use POST, but I didn't find a nice way to make a POST-Request with a link (a href)
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult DeleteComment( int commentId, int blogentryId )
        {
            _service.DeleteComment( commentId );
            return RedirectToAction( "Blogentry", new {id = blogentryId} );
        }

        [Authorize(Roles = CustomRoles.Administrator)]
        public ActionResult DeleteBlogentry(int id)
        {
            _service.DeleteBlogentry(id);
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}