using System;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Core.Exceptions;
using Blog.Web.Models.Account;
using Blog.Web.Services;
using WebMatrix.WebData;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IBlogService _service = new BlogService();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (model.Password != model.PasswordConfirmed)
            {
                ModelState.AddModelError("DISSENTING_PASSWORD", "The given passwords do not correspond.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        _service.RegisterUser(model);
                    }
                    catch (DisplayNameAlreadyExistsException)
                    {
                        ModelState.AddModelError("DisplayNameAlreadyExists", "The given display name already exists.");
                        return View(model);
                    }
                    catch (EmailAlreadyExistsException)
                    {
                        ModelState.AddModelError("EmailAlreadyExists", "The given email address already exists.");
                        return View(model);
                    }
                    return View("Created", model);
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", GetErrorString(ex.StatusCode));
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View(_service.GetUserProfile(WebSecurity.CurrentUserId));
        }

        [HttpPost]
        public ActionResult EditProfile(UserProfileModel userProfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.StoreUserProfile(userProfile);
                }
                catch (DisplayNameAlreadyExistsException)
                {
                    ModelState.AddModelError("DisplayNameAlreadyExists", "The given display name already exists.");
                    return View(userProfile);
                }
                catch (EmailAlreadyExistsException)
                {
                    ModelState.AddModelError("EmailAlreadyExists", "The given email address already exists.");
                    return View(userProfile);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.StaySignedIn))
            {
                return RedirectToLocal(ReturnUrl);
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (model.NewPassword != model.NewPasswordConfirmed)
            {
                ModelState.AddModelError("DISSENTING_PASSWORD", "The given passwords do not correspond.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.CurrentPassword, model.NewPassword))
                    {
                        return RedirectToAction("EditProfile");
                    }
                    ModelState.AddModelError("WrongPassword", "The current password was wrong.");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ValidateUser(string token)
        {
            _service.ValidateUser(token);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordFirstStep()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPasswordFirstStep(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                _service.SendPasswordResetToken(model);
                return View();
            }
            else
            {
                return View("ResetPasswordFirstStepCompleted");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordSecondStep(string token)
        {
            var model = new ResetPasswordSecondStepModel(token);
            //            _service.ResetPasswordSecondStep(token);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPasswordSecondStep(ResetPasswordSecondStepModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.NewPasswordConfirmed)
                {
                    ModelState.AddModelError("DifferentPasswords", "Your confirmed password doesn't match.");
                }
                else
                {
                    _service.ResetPasswordSecondStep(model);
                    return View("ResetPasswordSecondStepCompleted");
                }
            }
            return View(model);
        }

        #region private methods

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private static string GetErrorString(MembershipCreateStatus createStatus)
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