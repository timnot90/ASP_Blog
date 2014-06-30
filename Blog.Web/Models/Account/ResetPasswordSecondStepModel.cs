using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.Annotations;

namespace Blog.Web.Models.Account
{
    public class ResetPasswordSecondStepModel
    {
        public string Token { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage="The new password is required.")]
        public string NewPassword { get; set; }

        [DisplayName("Password Confirmed")]
        [Required(ErrorMessage = "Please confirm the new password.")]
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