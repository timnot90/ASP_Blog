using System.Security.Cryptography.X509Certificates;
using Blog.Web.Models.Account;

namespace Blog.Web.Services.Account
{
    public interface IBlogAccountService
    {
        int SaveUserProfile(EditUserProfileModel model);
        int RegisterUser(RegisterModel model);
        EditUserProfileModel GetEditProfileModel(int id);
        bool ValidateRegistrationToken(string token);
        void SendPasswordResetToken(ResetPasswordModel model);
        void ResetPasswordSecondStep(ResetPasswordSecondStepModel model);
        UserProfileModel GetUserByName(string username);
    }
}