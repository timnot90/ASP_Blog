using System;
using System.Linq;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Models.Account;
using WebMatrix.WebData;

namespace Blog.Web.Services.Account
{
    public class BlogAccountService : IBlogAccountService
    {
        #region variables
        private readonly IBlogRepository _repository = new BlogRepository();
        #endregion

        #region UserProfile

        public int SaveUserProfile(EditUserProfileModel model)
        {
            var userProfile = _repository.GetUserProfile(model.Id);
            model.UpdateSource(userProfile);
            return _repository.SaveUserProfile(userProfile);
        }

        public int RegisterUser(RegisterModel registerModel)
        {
            if (_repository.EmailExists(registerModel.Email))
            {
                throw new EmailAlreadyExistsException();
            }
            if (_repository.DisplayNameExists(registerModel.DisplayName))
            {
                throw new DisplayNameAlreadyExistsException();
            }
            string token = WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password,
                new
                {
                    UserNameLowercase = registerModel.UserName.ToLower(),
                    registerModel.DisplayName,
                    EMail = registerModel.Email,
                    EmailLowercase = registerModel.Email.ToLower(),
                }, true);

            MailSender.SendAccountValidationToken(token, registerModel.Email);

            UserProfile newProfile = new UserProfile();
            registerModel.UpdateSource(newProfile);
            return _repository.SaveUserProfile(newProfile, true);
        }

        public EditUserProfileModel GetEditProfileModel(int id)
        {
            UserProfile userProfile = _repository.GetUserProfile(id);
            EditUserProfileModel editUserProfileModel = userProfile == null ? null : new EditUserProfileModel(userProfile);
            return editUserProfileModel;
        }

        public bool ValidateRegistrationToken(string token)
        {
            try
            {
                return WebSecurity.ConfirmAccount(token);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            //            string userName = ""; 
            //            Roles.AddUserToRole( userName, CustomRoles.User );
        }

        public void SendPasswordResetToken(ResetPasswordModel model)
        {
            try
            {
                UserProfile userProfile =
                    _repository.GetAllUserProfiles().First(u => u.EmailLowercase == model.Email.ToLower());
                if (userProfile != null)
                {
                    string passwordResetToken = WebSecurity.GeneratePasswordResetToken(userProfile.UserName);
                    MailSender.SendAccountValidationToken(passwordResetToken, model.Email);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void ResetPasswordSecondStep(ResetPasswordSecondStepModel model)
        {
            try
            {
                WebSecurity.ResetPassword(model.Token, model.NewPassword);
            }
            catch (InvalidOperationException)
            {
            }
        }

        #endregion
    }
}