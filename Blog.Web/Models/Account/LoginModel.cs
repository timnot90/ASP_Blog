using System.ComponentModel;
using Blog.Web.Models.AccountModel;

namespace Blog.Web.Models.Account
{
    public class LoginModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("Password")]
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
            this.UserName = userProfile.UserName;
            this.Password = userProfile.Password;
        }
    }
}