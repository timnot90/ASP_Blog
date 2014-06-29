using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.Account
{
    public class ResetPasswordSecondStepModel
    {
        public string Token { get; set; }

        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Password Confirmed")]
        public string NewPasswordConfirmed { get; set; }

        public ResetPasswordSecondStepModel()
        {
        }
        public ResetPasswordSecondStepModel(string token)
        {
        }
    }
}