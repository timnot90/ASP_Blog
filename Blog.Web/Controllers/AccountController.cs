using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Core.Exceptions;
using Blog.Web.Models.Account;
using Blog.Web.Services.Account;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private static readonly IBlogAccountService Service = new BlogAccountService();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register( RegisterModel model )
        {
            if (model.Password != model.PasswordConfirmed)
            {
                ModelState.AddModelError( "DISSENTING_PASSWORD", "The given passwords do not correspond." );
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Service.RegisterUser( model );
                    return View( "Created", model );
                }
                catch (DisplayNameAlreadyExistsException)
                {
                    ModelState.AddModelError( "DisplayNameAlreadyExists", "The given display name already exists." );
                }
                catch (EmailAlreadyExistsException)
                {
                    ModelState.AddModelError( "EmailAlreadyExists", "The given email address already exists." );
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError( "MembershipCreateUserException", GetErrorString( ex.StatusCode ) );
                }
                catch (SmtpException)
                {
                    ModelState.AddModelError("SmtpError", "There was an error while sending your registration mail. Please contact the administrator.");
                }
            }
            return View( model );
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View( Service.GetEditProfileModel( WebSecurity.CurrentUserId ) );
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile( EditUserProfileModel userProfile )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveUserProfile( userProfile );
                }
                catch (DisplayNameAlreadyExistsException)
                {
                    ModelState.AddModelError( "DisplayNameAlreadyExists", "The given display name already exists." );
                    return View( userProfile );
                }
                catch (EmailAlreadyExistsException)
                {
                    ModelState.AddModelError( "EmailAlreadyExists", "The given email address already exists." );
                    return View( userProfile );
                }
                return RedirectToAction( "Index", "Home" );
            }
            return View( userProfile );
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction( "Index", "Home" );
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login( LoginModel model, string returnUrl )
        {
            UserProfileModel user = Service.GetUserByName( model.UserName );

            if (ModelState.IsValid && WebSecurity.Login( model.UserName, model.Password, model.StaySignedIn ))
            {
                if (user != null && !user.IsLocked)
                {
                    return RedirectToLocal( returnUrl );
                }
                ModelState.AddModelError( "AccountLocked",
                    "Your account is currently locked. Please contact us to unlock it." );
                WebSecurity.Logout();
                return View( model );
            }
            ModelState.AddModelError( "WrongUsernameOrPassword", "The user name or password provided is incorrect." );
            return View( model );
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword( ChangePasswordModel model )
        {
            if (model.NewPassword != model.NewPasswordConfirmed)
            {
                ModelState.AddModelError( "DISSENTING_PASSWORD", "The given passwords do not correspond." );
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (WebSecurity.ChangePassword( WebSecurity.CurrentUserName, model.CurrentPassword,
                        model.NewPassword ))
                    {
                        return RedirectToAction( "EditProfile" );
                    }
                    ModelState.AddModelError( "WrongPassword", "The current password was wrong." );
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError( "", ex.Message );
            }

            return View( model );
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ValidateRegistrationToken( string token )
        {
            try
            {
                if (Service.ValidateRegistrationToken( token ))
                {
                    return View( "AccountActivated" );
                }
            }
            catch (SmtpException)
            {
                ModelState.AddModelError("SmtpError", "There was an error while sending your registration mail. Please contact the administrator.");
                return View(new RegistrationValidationModel {Success=false});
            }
            return RedirectToAction( "Index", "Home" );
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordFirstStep()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPasswordFirstStep( ResetPasswordModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SendPasswordResetToken( model );
                    return View();
                }
                catch (SmtpException)
                {
                    ModelState.AddModelError("SmtpError", "There was an error while sending your password reset mail. Please contact the administrator.");
                }
            }
            return View( "ResetPasswordFirstStepCompleted" );
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordSecondStep( string token )
        {
            ResetPasswordSecondStepModel model = new ResetPasswordSecondStepModel( token );
            return View( model );
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPasswordSecondStep( ResetPasswordSecondStepModel model )
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.NewPasswordConfirmed)
                {
                    ModelState.AddModelError( "DifferentPasswords", "Your confirmed password doesn't match." );
                }
                else
                {
                    Service.ResetPasswordSecondStep( model );
                    return View( "ResetPasswordSecondStepCompleted" );
                }
            }
            return View( model );
        }

        #region private methods

        private ActionResult RedirectToLocal( string returnUrl )
        {
            if (Url.IsLocalUrl( returnUrl ))
            {
                return Redirect( returnUrl );
            }
            return RedirectToAction( "Index", "Home" );
        }

        private static string GetErrorString( MembershipCreateStatus createStatus )
        {
            // Vollständige Liste Fehlercodes: http://go.microsoft.com/fwlink/?LinkID=177550 

            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "The username already exists.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "There is already a registered user with this e-mail adress.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password is invalid.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail adress is invalid.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The username is invalid";

                case MembershipCreateStatus.ProviderError:
                    return "The user couldn't be registered because of an internal error.";

                default:
                    return "An error occured.";
            }
        }

        #endregion
    }
}