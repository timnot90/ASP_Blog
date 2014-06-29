using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Web.ModelValidators;
using Blog.Web.ModelValidators.Account;
using FluentValidation.Mvc;

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