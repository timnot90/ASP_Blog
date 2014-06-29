using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Shared;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Shared
{
    [Validator(typeof(BlogSettingsModelValidator))]
    public class BlogSettingsModel
    {
        public bool SuccessfullySaved { get; set; }

        [DisplayName("Site Name")]
        public string SiteName { get; set; }

        [DisplayName("Number Of Blogentries Per Page")]
        public int NumberOfEntriesPerPage { get; set; }

        [DisplayName("Keywords (separated by ',')")]
        public string Keywords { get; set; }

        [DisplayName("Subject")]
        public string RegistrationMailSubject { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        public string RegistrationMailBody { get; set; }

        [DisplayName("Sender")]
        public string RegistrationMailSender { get; set; }

        [DisplayName("Subject")]
        public string PasswordChangeMailSubject { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        public string PasswordChangeMailBody { get; set; }

        [DisplayName("Sender")]
        public string PasswordChangeMailSender { get; set; }

        [DisplayName("Subject")]
        public string WelcomeMailSubject { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        public string WelcomeMailBody { get; set; }

        [DisplayName("Sender")]
        public string WelcomeMailSender { get; set; }

        [DisplayName("Footer Text")]
        [AllowHtml]
        public string FooterText { get; set; }

        [DisplayName("Url")]
        public string SmtpServerUrl { get; set; }

        [DisplayName("Port")]
        public int SmtpServerPort { get; set; }

        [DisplayName("Username")]
        public string SmtpServerUsername { get; set; }

        [DisplayName("Password")]
        public string SmtpServerPassword{ get; set; }

        [DisplayName("User Credentials Mandatory for Login")]
        public bool SmtpAreUsercredentialsMandatoryForLogin { get; set; }

        [DisplayName("Activate Comments")]
        public bool CommentsActivated { get; set; }

        public BlogSettingsModel()
        {
        }

        public BlogSettingsModel(Setting settings)
        {
            UpdateModel( settings );
        }

        public void UpdateSource(Setting source)
        {
            source.SiteName = SiteName;
            source.NumberOfEntriesPerPage = NumberOfEntriesPerPage;
            source.Keywords = Keywords;
            source.RegistrationMailSubject = RegistrationMailSubject;
            source.RegistrationMailBody = RegistrationMailBody;
            source.RegistrationMailSender = RegistrationMailSender;
            source.PasswordChangeMailSubject = PasswordChangeMailSubject;
            source.PasswordChangeMailBody = PasswordChangeMailBody;
            source.PasswordChangeMailSender = PasswordChangeMailSender;
            source.WelcomeMailSubject = WelcomeMailSubject;
            source.WelcomeMailBody = WelcomeMailBody;
            source.WelcomeMailSender = WelcomeMailSender;
            source.FooterText = FooterText;
            source.SmtpServerAddress = SmtpServerUrl;
            source.SmtpServerPort= SmtpServerPort;
            source.SmtpServerUsername = SmtpServerUsername;
            source.SmtpServerPassword = SmtpServerPassword;
            source.SmtpIsPasswordMandatoryForLogin = SmtpAreUsercredentialsMandatoryForLogin;
            source.CommentsActivated = CommentsActivated;
        }

        public void UpdateModel( Setting source )
        {
            SiteName = source.SiteName;
            NumberOfEntriesPerPage = source.NumberOfEntriesPerPage;
            Keywords = source.Keywords;
            RegistrationMailSubject = source.RegistrationMailSubject;
            RegistrationMailBody = source.RegistrationMailBody;
            RegistrationMailSender = source.RegistrationMailSender;
            PasswordChangeMailSubject = source.PasswordChangeMailSubject;
            PasswordChangeMailBody = source.PasswordChangeMailBody;
            PasswordChangeMailSender = source.PasswordChangeMailSender;
            WelcomeMailSubject = source.WelcomeMailSubject;
            WelcomeMailBody = source.WelcomeMailBody;
            WelcomeMailSender = source.WelcomeMailSender;
            FooterText = source.FooterText;
            SmtpServerUrl = source.SmtpServerAddress;
            SmtpServerPort = source.SmtpServerPort;
            SmtpServerUsername = source.SmtpServerUsername;
            SmtpServerPassword = source.SmtpServerPassword;
            SmtpAreUsercredentialsMandatoryForLogin = source.SmtpIsPasswordMandatoryForLogin;
            CommentsActivated = source.CommentsActivated;
        }
    }
}