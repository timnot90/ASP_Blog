using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.Account
{
    public class ResetPasswordSecondStepModel
    {
        public ResetPasswordSecondStepModel()
        {
        }
        public ResetPasswordSecondStepModel(string token)
        {
        }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmed { get; set; }
    }
}