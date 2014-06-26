using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Administration.Models
{
    public class ChangeSmtpPasswordModel
    {
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        [Required]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        public string NewPasswordConfirmed { get; set; }
    }
}