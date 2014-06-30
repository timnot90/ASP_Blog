using System.ComponentModel;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(ChangePasswordModelValidator))]
    public class ChangePasswordModel
    {
        [DisplayName("Current Password")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string NewPasswordConfirmed { get; set; }
    }
}