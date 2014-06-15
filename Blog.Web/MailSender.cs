using System;
using System.Net;
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
                blogSettings.RegistrationMailBody.Replace(GlobalValues.RegistrationMailPlaceholderActivationLink,
                    linkTag.ToString()).Replace(GlobalValues.RegistrationMailPlaceholderUsername, username);

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
                blogSettings.PasswordChangeMailBody.Replace(GlobalValues.PasswordChangeMailPlaceholderSecondStepLink,
                    link.ToString()).Replace(GlobalValues.PasswordChangeMailPlaceholderUsername, username);

            SendMail(blogSettings.PasswordChangeMailSender, recipient, blogSettings.PasswordChangeMailSubject, mailBody);
        }

        public static void SendWelcomeMail( string recipient, string username )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();

            string mailBody = blogSettings.WelcomeMailBody.Replace(GlobalValues.WelcomeMailPlaceholderUsername, username);

            SendMail(blogSettings.WelcomeMailSender, recipient, blogSettings.WelcomeMailSubject, mailBody);
        }

        private static void SendMail( string sender, string recipient, string subject, string body )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            SmtpClient.Host = blogSettings.SmtpServerAddress;
            SmtpClient.Port = 587;
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
            mail.Priority = MailPriority.High;

            var mailBody = new StringBuilder();
            mailBody.AppendLine( "<html>" );
            mailBody.AppendLine( "<head>" );
            mailBody.AppendLine( "</head>" );
            mailBody.AppendLine( "<body>" );
            mailBody.AppendLine( body );
            mailBody.AppendLine( "</body>" );
            mailBody.AppendLine( "</html>" );

            mail.IsBodyHtml = true;
            mail.Body = mailBody.ToString();
            mail.Subject = subject;

            SmtpClient.Send( mail );
        }
    }
}