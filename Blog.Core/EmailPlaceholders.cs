namespace Blog.Core
{
    public static class EmailPlaceholders
    {
        public const string RegistrationMailPlaceholderUsername = "#UserName#";
        public const string RegistrationMailPlaceholderActivationLink = "#ActivationLink#";
        public const string WelcomeMailPlaceholderUsername = "#UserName#";
        public const string PasswordChangeMailPlaceholderUsername = "#UserName#";
        public const string PasswordChangeMailPlaceholderSecondStepLink = "#LinkToPasswordChange#";
    }
}