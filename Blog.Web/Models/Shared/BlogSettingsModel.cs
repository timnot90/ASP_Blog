using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Shared;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Shared
{
    //[Validator(typeof(BlogSettingsModelValidator))]
    public class BlogSettingsModel
    {
        public bool SuccessfullySaved { get; set; }

        [DisplayName("Site Name")]
        [Required(ErrorMessage = "The Sitename cannot be empty.")]
        public string SiteName { get; set; }

        [DisplayName("Number Of Blogentries Per Page")]
        [Required(ErrorMessage = "You have to specify the number of entries per page.")]
        public int NumberOfEntriesPerPage { get; set; }

        [DisplayName("Keywords (separated by ',')")]
        [RegularExpression(@"[A-Za-z0-9]+(,[A-Za-z0-9\s]*)*[A-Za-z0-9]+", ErrorMessage = "The Keywords do not have a valid format.")]
        public string Keywords { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify a subject for the registration mails.")]
        public string RegistrationMailSubject { get; set; }

        [DisplayName("Body")]
        [Required(ErrorMessage = "You have to specify a body for the registration mails.")]
        [AllowHtml]
        public string RegistrationMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify a sender for the registration mails.")]
        public string RegistrationMailSender { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify a subject for the password change mails.")]
        public string PasswordChangeMailSubject { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        [Required(ErrorMessage = "You have to specify a body for the password change mails.")]
        public string PasswordChangeMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify a sender for the password change mails.")]
        public string PasswordChangeMailSender { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify a subject for the welcome mails.")]
        public string WelcomeMailSubject { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        [Required(ErrorMessage = "You have to specify a body for the welcome mails.")]
        public string WelcomeMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify a sender for the welcome mails.")]
        public string WelcomeMailSender { get; set; }

        [DisplayName("Footer Text")]
        [AllowHtml]
        [Required(ErrorMessage = "You have to specify a footer text for the site.")]
        public string FooterText { get; set; }

        [DisplayName("Url")]
        [Required(ErrorMessage = "You have to specify the URL of the SMTP server.")]
        public string SmtpServerUrl { get; set; }

        [DisplayName("Port")]
        [Required(ErrorMessage = "You have to specify the port of the SMTP server")]
        public int SmtpServerPort { get; set; }

        [DisplayName("Username")]
        public string SmtpServerUsername { get; set; }

        [DisplayName("Password")]
        public string SmtpServerPassword{ get; set; }

        [DisplayName("User Credentials Mandatory for Login")]
        public bool SmtpAreUsercredentialsMandatoryForLogin { get; set; }

        [DisplayName("Activate Comments")]
        public bool CommentsActivated { get; set; }

        [UsedImplicitly]
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

        // ReSharper disable once MemberCanBePrivate.Global
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