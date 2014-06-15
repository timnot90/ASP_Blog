using System;
using System.Linq;
using System.Web.Security;
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
            var userProfile = _repository.GetUserProfileById(model.Id);

            string emailLowercase = model.Email.ToLower();

            // when the email adress has changed and there is already another user with that address
            if (!userProfile.EmailLowercase.Equals(model.Email) && _repository.GetAllUserProfiles().Any(u => u.EmailLowercase.Equals(emailLowercase)))
            {
                throw new EmailAlreadyExistsException();
            }
            // when the displayname has changed and there is already another user with that address
            if (!userProfile.DisplayName.Equals(model.DisplayName) && _repository.GetAllUserProfiles().Any(u => u.DisplayName.Equals(model.DisplayName)))
            {
                throw new DisplayNameAlreadyExistsException();
            }

            model.UpdateSource(userProfile);
            return _repository.SaveUserProfile(userProfile);
        }

        public int RegisterUser(RegisterModel model)
        {
            if (_repository.EmailExists(model.Email))
            {
                throw new EmailAlreadyExistsException();
            }
            if (_repository.DisplayNameExists(model.DisplayName))
            {
                throw new DisplayNameAlreadyExistsException();
            }
            string token = WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                new
                {
                    UserNameLowercase = model.UserName.ToLower(),
                    model.DisplayName,
                    EMail = model.Email,
                    EmailLowercase = model.Email.ToLower(),
                    IsLocked = false
                }, true);

            MailSender.SendRegistrationToken(token, model.Email, model.UserName);

            var newProfile = new UserProfile();
            model.UpdateSource(newProfile);

            Roles.AddUserToRole( newProfile.UserName, CustomRoles.User );

            return _repository.GetUserProfileByUsername( model.UserName ).ID;
                //_repository.SaveUserProfile(newProfile, true);
        }

        public EditUserProfileModel GetEditProfileModel(int id)
        {
            UserProfile userProfile = _repository.GetUserProfileById(id);
            EditUserProfileModel editUserProfileModel = userProfile == null ? null : new EditUserProfileModel(userProfile);
            return editUserProfileModel;
        }

        public bool ValidateRegistrationToken(string token)
        {
            try
            {
                UserProfile correspondingUser = _repository.GetUserByRegistrationToken( token );
                if (correspondingUser == null) return false;

                MailSender.SendWelcomeMail( correspondingUser.Email, correspondingUser.UserName );
                return WebSecurity.ConfirmAccount( token );
            }
            catch (InvalidOperationException)
            {
                return false;
            }
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
                    MailSender.SendRegistrationToken(passwordResetToken, model.Email, model.Username);
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

        public UserProfileModel GetUserByName(string username)
        {
            UserProfile user = _repository.GetAllUserProfiles().FirstOrDefault( u => u.UserName == username );
            return user == null ? null : new UserProfileModel( user );
        }
        #endregion
    }
}