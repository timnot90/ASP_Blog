using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    //[FluentValidation.Attributes.Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        [DisplayName("E-Mail")]
        [Required(ErrorMessage="Plese enter your E-Mail Address")]
        [EmailAddress(ErrorMessage="The format of the entered E-Mail Address is invalid.")]
        public string Email { get; set; }
    }
}