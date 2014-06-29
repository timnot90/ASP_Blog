using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [DisplayName("Username")]
        public string Username { get; set; }
    }
}