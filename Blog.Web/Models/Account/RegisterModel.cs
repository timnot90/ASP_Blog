using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(RegisterModelValidator))]
    public class RegisterModel
    {

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("New Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string PasswordConfirmed { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }

        public void UpdateSource(UserProfile source)
        {
            source.UserName = UserName;
            source.UserNameLowercase = UserName.ToLower();
            source.Email = Email;
            source.EmailLowercase = Email.ToLower();
            source.DisplayName = DisplayName;
        }

    }
}