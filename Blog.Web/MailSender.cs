﻿using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Blog.Core;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;

namespace Blog.Web
{
    public static class MailSender
    {
        private static readonly BlogRepository BlogRepository = new BlogRepository();
        private static readonly SmtpClient SmtpClient = new SmtpClient();

        public static void SendRegistrationToken( string token, string recipient, string username )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port +
                              "/Account/ValidateRegistrationToken?token=" + token;
            var linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("href", linkText);
            linkTag.InnerHtml += linkText;

            string mailBody =
                blogSettings.RegistrationMailBody.Replace(EmailPlaceholders.RegistrationMailPlaceholderActivationLink,
                    linkTag.ToString()).Replace(EmailPlaceholders.RegistrationMailPlaceholderUsername, username);

            SendMail(blogSettings.RegistrationMailSender, recipient, blogSettings.RegistrationMailSubject, mailBody);
        }

        public static void SendPasswordResetToken( string token, string recipient, string username )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port +
                              "/Account/ResetPasswordSecondStep?token=" + token;
            var link = new TagBuilder("a");
            link.Attributes.Add("href", linkText);
            link.InnerHtml = linkText;

            string mailBody =
                blogSettings.PasswordChangeMailBody.Replace(EmailPlaceholders.PasswordChangeMailPlaceholderSecondStepLink,
                    link.ToString()).Replace(EmailPlaceholders.PasswordChangeMailPlaceholderUsername, username);

            SendMail(blogSettings.PasswordChangeMailSender, recipient, blogSettings.PasswordChangeMailSubject, mailBody);
        }

        public static void SendWelcomeMail( string recipient, string username )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();

            string mailBody = blogSettings.WelcomeMailBody.Replace(EmailPlaceholders.WelcomeMailPlaceholderUsername, username);

            SendMail(blogSettings.WelcomeMailSender, recipient, blogSettings.WelcomeMailSubject, mailBody);
        }

        private static void SendMail( string sender, string recipient, string subject, string body )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            SmtpClient.Host = blogSettings.SmtpServerAddress;
            SmtpClient.Port = blogSettings.SmtpServerPort;
            SmtpClient.EnableSsl = true;
            if (blogSettings.SmtpIsPasswordMandatoryForLogin)
            {
                SmtpClient.Credentials = new NetworkCredential( blogSettings.SmtpServerUsername,
                    blogSettings.SmtpServerPassword );
            }

            var mail = new MailMessage();
            mail.To.Add( new MailAddress( recipient ) );
            mail.Sender = new MailAddress( sender );
            mail.From = new MailAddress(sender);

            mail.IsBodyHtml = true;

            var mailBody = new StringBuilder();
            mailBody.AppendLine( "<html>" );
            mailBody.AppendLine( "<head>" );
            mailBody.AppendLine( "</head>" );
            mailBody.AppendLine( "<body>" );
            mailBody.AppendLine( body );
            mailBody.AppendLine( "</body>" );
            mailBody.AppendLine( "</html>" );

            mail.Body = mailBody.ToString();
            mail.Subject = subject;

            SmtpClient.Send( mail );
        }
    }
}