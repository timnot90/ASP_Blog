using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.AccountModel
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