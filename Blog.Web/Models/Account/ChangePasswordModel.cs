using System.ComponentModel;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(ChangePasswordModelValidator))]
    public class ChangePasswordModel
    {
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string NewPasswordConfirmed { get; set; }
    }
}