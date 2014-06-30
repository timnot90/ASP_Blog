using System.ComponentModel;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        [DisplayName("E-Mail")]
        public string Email { get; set; }
    }
}