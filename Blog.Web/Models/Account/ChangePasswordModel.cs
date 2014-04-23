using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Account
{
    public class ChangePasswordModel
    {
        [DisplayName("Current Password")]
        [Required]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        [Required]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        public string NewPasswordConfirmed { get; set; }
    }
}