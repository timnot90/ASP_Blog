using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Account
{
    public class ResetPasswordModel
    {
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage = "The format of the entered email-address is not valid.")]
        [Required(ErrorMessage = "Please enter your email-address")]
        public string Email { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }
    }
}