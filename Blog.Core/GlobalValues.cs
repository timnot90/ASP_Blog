using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core
{
    public class GlobalValues
    {
        public const string RegistrationMailPlaceholderUsername = "#UserName#";
        public const string RegistrationMailPlaceholderActivationLink = "#ActivationLink#";
        public const string WelcomeMailPlaceholderUsername = "#UserName#";
        public const string PasswordChangeMailPlaceholderUsername = "#UserName#";
        public const string PasswordChangeMailPlaceholderSecondStepLink = "#LinkToPasswordChange#";
    }
}