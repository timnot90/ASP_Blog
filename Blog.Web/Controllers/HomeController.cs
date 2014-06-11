using System.Web.Mvc;
using Blog.Web.Models.Home;
using Blog.Web.Services.Home;
using Recaptcha;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables

        private readonly IBlogHomeService _service = new BlogHomeService();

        #endregion

        #region views

        [AllowAnonymous]
        public ActionResult Index( int? categoryId, string monthAndYear, string searchText )
        {
            if (!categoryId.HasValue)
            {
                categoryId = 0;
            }
//            if (categoryId == 0 && string.IsNullOrEmpty( monthAndYear ))
//            {
//                return View( _service.GetBlogentryListModel() );
//            }
            return View( _service.GetBlogentryListModel( (int) categoryId, monthAndYear, searchText ) );
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
        [ValidateInput( false )]
        public ActionResult AddBlogentry( AddBlogentryModel blogentry )
        {
            if (ModelState.IsValid)
            {
                int id = _service.CreateNewBlogentry( blogentry );
                return RedirectToAction( "Blogentry", new {id} );
            }
            return View( blogentry );
        }

        [HttpPost]
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult DeleteCategory( int categoryid )
        {
            _service.DeleteCategory( categoryid );
            return RedirectToAction( "Categories" );
        }

        #endregion

        #region partial views

        [HttpPost]
        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        [AllowAnonymous]
        public ActionResult _LeaveComment( LeaveCommentModel comment, bool captchaValid, string captchaErrorMessage )
        {
            if (!WebSecurity.IsAuthenticated && !captchaValid)
            {
                ModelState.AddModelError( "captcha", captchaErrorMessage );
            }
            if (ModelState.IsValid)
            {
                _service.StoreComment( comment );
                return RedirectToAction( "Blogentry", new {id = comment.BlogentryId} );
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult _BlogSidebar()
        {
            return PartialView( _service.GetBlogSidebarModel() );
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult _BlogSidebar( string searchText )
        {
            return Index( null, null, searchText );
        }

        [HttpGet]
        [Authorize( Roles = CustomRoles.Administrator )]
        public PartialViewResult AddCategory()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize( Roles = CustomRoles.Administrator )]
        public ActionResult AddCategory( CategoryModel model )
        {
            if (ModelState.IsValid)
            {
                _service.StoreCategory( model );
                return RedirectToAction( "Categories" );
            }
            //TODO: add and show error when model state is not valid
            return RedirectToAction( "Categories", model );
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
        public ActionResult DeleteComment( int commentId )
        {
            int blogentryId = _service.GetComment( commentId ).BlogentryId;
            _service.DeleteComment( commentId );
            return RedirectToAction( "Blogentry", new {id = blogentryId} );
        }
    }
}