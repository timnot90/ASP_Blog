using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models
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
        [Required(ErrorMessage = "The username for the SMTP-server  cannot be empty.")]
        public string SmtpServerUsername { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "The password for the SMTP-Server cannot be empty.")]
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

        public void UpdateSource(Setting settings)
        {
            settings.SiteName = SiteName;
            settings.NumberOfEntriesPerPage = NumberOfEntriesPerPage;
            settings.RegistrationMailSubject = RegistrationMailSubject;
            settings.RegistrationMailBody = RegistrationMailBody;
            settings.RegistrationMailSender = RegistrationMailSender;
            settings.PasswordChangeMailSubject = PasswordChangeMailSubject;
            settings.PasswordChangeMailBody = PasswordChangeMailBody;
            settings.PasswordChangeMailSender = PasswordChangeMailSender;
            settings.WelcomeMailSubject = WelcomeMailSubject;
            settings.WelcomeMailBody = WelcomeMailBody;
            settings.WelcomeMailSender = WelcomeMailSender;
            settings.FooterText = FooterText;
            settings.SmtpServerAddress = SmtpServerAddress;
            settings.SmtpServerUsername = SmtpServerUsername;
            settings.SmtpServerPassword = SmtpServerPassword;
            settings.SmtpIsPasswordMandatoryForLogin = SmtpIsPasswordMandatoryForLogin;
            settings.CommentsActivated = CommentsActivated;
        }

        public void UpdateModel( Setting settings )
        {
            SiteName = settings.SiteName;
            NumberOfEntriesPerPage = settings.NumberOfEntriesPerPage;
            RegistrationMailSubject = settings.RegistrationMailSubject;
            RegistrationMailBody = settings.RegistrationMailBody;
            RegistrationMailSender = settings.RegistrationMailSender;
            PasswordChangeMailSubject = settings.PasswordChangeMailSubject;
            PasswordChangeMailBody = settings.PasswordChangeMailBody;
            PasswordChangeMailSender = settings.PasswordChangeMailSender;
            WelcomeMailSubject = settings.WelcomeMailSubject;
            WelcomeMailBody = settings.WelcomeMailBody;
            WelcomeMailSender = settings.WelcomeMailSender;
            FooterText = settings.FooterText;
            SmtpServerAddress = settings.SmtpServerAddress;
            SmtpServerUsername = settings.SmtpServerUsername;
            SmtpServerPassword = settings.SmtpServerPassword;
            SmtpIsPasswordMandatoryForLogin = settings.SmtpIsPasswordMandatoryForLogin;
            CommentsActivated = settings.CommentsActivated;
        }
    }
}