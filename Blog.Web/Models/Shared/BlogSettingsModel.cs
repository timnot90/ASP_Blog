using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Shared
{
    public class BlogSettingsModel
    {
        public bool SuccessfullySaved { get; set; }

        [DisplayName("Site Name")]
        [Required(ErrorMessage = "The Site Name cannot be empty.")]
        public string SiteName { get; set; }

        [DisplayName("Number Of Blogentries Per Page")]
        [Required(ErrorMessage = "You have to specify the number of blogentries that should be displayed per page.")]
        public int NumberOfEntriesPerPage { get; set; }

        [RegularExpression(@"[A-Za-z0-9]+(,[A-Za-z0-9\s]*)*[A-Za-z0-9]+", ErrorMessage = "The Keywords do not have a valid format.")]
        [DisplayName("Keywords (separated by ',')")]
        public string Keywords { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify the subject of the registration mails.")]
        public string RegistrationMailSubject { get; set; }

        [DisplayName("Body")]
        [Required(ErrorMessage = "You have to specify the body of the registration mails.")]
        public string RegistrationMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify the sender of the registration mails.")]
        public string RegistrationMailSender { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify the subject of the password change mails.")]
        public string PasswordChangeMailSubject { get; set; }

        [DisplayName("Body")]
        [Required(ErrorMessage = "You have to specify the body of the password change mails.")]
        public string PasswordChangeMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify the sender of the password change mails.")]
        public string PasswordChangeMailSender { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You have to specify the subject of the welcome mails.")]
        public string WelcomeMailSubject { get; set; }

        [DisplayName("Body")]
        [Required(ErrorMessage = "You have to specify the body of the welcome mails.")]
        public string WelcomeMailBody { get; set; }

        [DisplayName("Sender")]
        [Required(ErrorMessage = "You have to specify the sender of the welcome mails.")]
        public string WelcomeMailSender { get; set; }

        [DisplayName("Footer Text")]
        [Required(ErrorMessage = "The Footer Text cannot be empty.")]
        public string FooterText { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "The SMTP-Server Address cannot be empty.")]
        public string SmtpServerAddress { get; set; }

        [DisplayName("Username")]
        public string SmtpServerUsername { get; set; }

        [DisplayName("Password")]
        public string SmtpServerPassword { get; set; }

        [DisplayName("Password is Mandatory for Login")]
        public bool SmtpIsPasswordMandatoryForLogin { get; set; }

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
            source.SmtpServerAddress = SmtpServerAddress;
            source.SmtpServerUsername = SmtpServerUsername;
            source.SmtpServerPassword = SmtpServerPassword;
            source.SmtpIsPasswordMandatoryForLogin = SmtpIsPasswordMandatoryForLogin;
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
            SmtpServerAddress = source.SmtpServerAddress;
            SmtpServerUsername = source.SmtpServerUsername;
            SmtpServerPassword = source.SmtpServerPassword;
            SmtpIsPasswordMandatoryForLogin = source.SmtpIsPasswordMandatoryForLogin;
            CommentsActivated = source.CommentsActivated;
        }
    }
}