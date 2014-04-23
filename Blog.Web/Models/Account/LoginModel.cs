using System.ComponentModel;

namespace Blog.Web.Models.Account
{
    public class LoginModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("NewPassword")]
        public string Password { get; set; }

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