using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    //[FluentValidation.Attributes.Validator(typeof(RegisterModelValidator))]
    public class RegisterModel
    {

        [DisplayName("Username")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "The Username cannot be empty.")]
        public string UserName { get; set; }

        [DisplayName("Displayname")]
        [StringLength(50, ErrorMessage = "Displayname cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "The Displayname cannot be empty.")]
        public string DisplayName { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "The New Password cannot be empty.")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "The Confirmed Password cannot be empty.")]
        public string PasswordConfirmed { get; set; }

        [DisplayName("E-Mail")]
        [StringLength(100, ErrorMessage = "E-Mail Address cannot be longer than 100 characters.")]
        [Required(ErrorMessage = "The E-Mail Address cannot be empty.")]
        [EmailAddress(ErrorMessage = "The format of the entered E-Mail Address is invalid.")]
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