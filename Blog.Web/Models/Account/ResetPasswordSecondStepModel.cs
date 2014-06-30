using System.ComponentModel;
using Blog.Core.Annotations;

namespace Blog.Web.Models.Account
{
    public class ResetPasswordSecondStepModel
    {
        public string Token { get; set; }

        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Password Confirmed")]
        public string NewPasswordConfirmed { get; set; }

        [UsedImplicitly]
        public ResetPasswordSecondStepModel()
        {
        }
        public ResetPasswordSecondStepModel(string token)
        {
        }
    }
}