using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    //[FluentValidation.Attributes.Validator(typeof(LoginModelValidator))]
    public class LoginModel
    {
        [DisplayName("Username")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "The Username cannot be empty.")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "The Password cannot be empty.")]
        public string Password { get; set; }

        [DisplayName("Stay signed in")]
        public bool StaySignedIn { get; set; }
    }
}