using System.ComponentModel;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(LoginModelValidator))]
    public class LoginModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Stay signed in")]
        public bool StaySignedIn { get; set; }

        public LoginModel()
        {
        }
    }
}