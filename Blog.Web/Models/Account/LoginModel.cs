using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Account
{
    public class LoginModel
    {
        [DisplayName("Username")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("NewPassword")]
        [Required]
        public string Password { get; set; }

        [DisplayName("Stay signed in")]
        public bool StaySignedIn { get; set; }

        public LoginModel()
        {
        }

        public LoginModel(UserProfileModel userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateModel(UserProfileModel userProfile)
        {
            UserName = userProfile.UserName;
        }
    }
}