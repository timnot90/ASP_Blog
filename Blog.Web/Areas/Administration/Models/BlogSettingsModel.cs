using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models
{
    public class BlogSettingsModel
    {
        public string SiteName { get; set; }
        public int NumberOfEntriesPerPage { get; set; }
        public string RegistrationMailSubject { get; set; }
        public string RegistrationMailBody { get; set; }
        public string RegistrationMailSender { get; set; }
        public string PasswordChangeMailSubject { get; set; }
        public string PasswordChangeMailBody { get; set; }
        public string PasswordChangeMailSender { get; set; }
        public string WelcomeMailSubject { get; set; }
        public string WelcomeMailBody { get; set; }
        public string WelcomeMailSender { get; set; }
        public string FooterText { get; set; }
        public string SmtpServerAddress { get; set; }
        public string SmtpServerUsername { get; set; }
        public string SmtpServerPassword { get; set; }
        public bool SmtpIsPasswordMandatoryForLogin { get; set; }
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