using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators;
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