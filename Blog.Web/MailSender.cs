using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;

namespace Blog.Web
{
    public static class MailSender
    {
        private static readonly BlogRepository BlogRepository = new BlogRepository();
        private static readonly SmtpClient SmtpClient = new SmtpClient();

        public static void SendRegistrationToken( string token, string recipient )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port +
                              "/Account/ValidateRegistrationToken?token=" + token;
            var linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("href", linkText);
            linkTag.InnerHtml += linkText;

            SendMail(blogSettings.RegistrationMailSender, recipient, blogSettings.RegistrationMailSubject, String.Format( blogSettings.RegistrationMailBody, linkTag.ToString() ));

//            StringBuilder mailBody = new StringBuilder();
//            mailBody.AppendLine( "<html>" );
//            mailBody.AppendLine( "<head>" );
//            mailBody.AppendLine( "</head>" );
//            mailBody.AppendLine( "<body>" );
//            //mailBody.AppendLine("Click on the following link or copy it into the address bar of your browser in order to reset your password:<br/>");
//            mailBody.AppendLine( String.Format( blogSettings.RegistrationMailBody, linkTag ) );
//            mailBody.AppendLine( "</body>" );
//            mailBody.AppendLine( "</html>" );

//            mail.IsBodyHtml = true;
//            mail.Body = mailBody.ToString();
//            mail.Subject = "Subject";

//            Smtp.Send( mail );
        }

        public static void SendPasswordResetToken( string token, string recipient )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port +
                              "/Account/ResetPasswordSecondStep?token=" + token;
            var link = new TagBuilder("a");
            link.Attributes.Add("href", linkText);
            link.InnerHtml = linkText;

            SendMail(blogSettings.PasswordChangeMailSender, recipient, blogSettings.PasswordChangeMailSubject, String.Format(blogSettings.PasswordChangeMailBody, linkText));
        }

        public static void SendWelcomeMail( string recipient )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();

            SendMail(blogSettings.WelcomeMailSender, recipient, blogSettings.WelcomeMailSubject, blogSettings.WelcomeMailBody);
        }

        private static void SendMail( string sender, string recipient, string subject, string body )
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();
            SmtpClient.Host = blogSettings.SmtpServerAddress;
            if (blogSettings.SmtpIsPasswordMandatoryForLogin)
            {
                SmtpClient.Credentials = new NetworkCredential( blogSettings.SmtpServerUsername,
                    blogSettings.SmtpServerPassword );
            }

            var mail = new MailMessage();
            mail.To.Add( new MailAddress( recipient ) );
            mail.Sender = new MailAddress( sender );
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