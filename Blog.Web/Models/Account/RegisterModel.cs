using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Account
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "The username must be declared.")]
        public string UserName { get; set; }

        [DisplayName("Display Name")]
        [Required(ErrorMessage = "The display name must be declared.")]
        public string DisplayName { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "A password must be declared.")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Your password must be confirmed.")]
        public string PasswordConfirmed { get; set; }

        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage = "The format of the entered E-Mail adress is not valid.")]
//        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "The E-Mail Adress is not valid.")]
        [Required(ErrorMessage = "An E-Mail adress must be declared.")]
        public string Email { get; set; }

        public RegisterModel()
        {
        }

        public RegisterModel(UserProfile userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateSource(UserProfile source)
        {
            source.UserName = UserName;
            source.UserNameLowercase = UserName.ToLower();
            source.Email = Email;
            source.EmailLowercase = Email.ToLower();
            source.DisplayName = DisplayName;
        }

        public void UpdateModel(UserProfile source)
        {
            Id = source.ID;
            UserName = source.UserName;
            DisplayName = source.DisplayName;
            Email = source.Email;
        }

    }
}