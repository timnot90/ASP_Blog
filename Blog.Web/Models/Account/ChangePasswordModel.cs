using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Web.ModelValidators.Account;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Account
{
    //[Validator(typeof(ChangePasswordModelValidator))]
    public class ChangePasswordModel
    {
        [DisplayName("Current Password")]
        [Required(ErrorMessage="The current password cannot be empty.")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        [Required]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string NewPasswordConfirmed { get; set; }
    }
}