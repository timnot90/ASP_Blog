using System;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Microsoft.Ajax.Utilities;

namespace Blog.Web
{
    public static class MailSender
    {
        private static readonly BlogRepository BlogRepository = new BlogRepository();
        private static readonly SmtpClient Smtp = new SmtpClient();

        public static void SendAccountValidationToken(string token, string recipient)
        {
            Setting blogSettings = BlogRepository.GetBlogSettings();

            Smtp.Credentials = new NetworkCredential( blogSettings.SmtpServerUsername, blogSettings.SmtpServerPassword );
            Smtp.Host = blogSettings.SmtpServerAddress;


            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress( blogSettings.RegistrationMailSender );
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Account/ValidateRegistrationToken?token=" + token;

            mail.To.Add(new MailAddress(recipient));
            mail.Priority = MailPriority.High;

            TagBuilder linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("href", linkText);
            linkTag.InnerHtml += linkText;

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine("<html>");
            mailBody.AppendLine("<head>");
            mailBody.AppendLine("</head>");
            mailBody.AppendLine("<body>");
            //mailBody.AppendLine("Click on the following link or copy it into the address bar of your browser in order to reset your password:<br/>");
            mailBody.AppendLine( String.Format(blogSettings.RegistrationMailBody, linkTag.ToString() ));
            mailBody.AppendLine("</body>");
            mailBody.AppendLine("</html>");

            mail.IsBodyHtml = true;
            mail.Body = mailBody.ToString();
            mail.Subject = "Subject";

            Smtp.Send(mail);
        }

        public static void SendPasswordResetToken(string token, string recipient)
        {
            MailMessage mail = new MailMessage();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Account/ResetPasswordSecondStep?token=" + token;

            mail.To.Add(new MailAddress(recipient));
            mail.Priority = MailPriority.High;

            TagBuilder linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("href", linkText);
            linkTag.InnerHtml += linkText;

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine("<html>");
            mailBody.AppendLine("<head>");
            mailBody.AppendLine("</head>");
            mailBody.AppendLine("<body>");
            mailBody.AppendLine("Click on the following link or copy it into the address bar of your browser in order to reset your password:<br/>");
            mailBody.AppendLine(linkTag.ToString());
            mailBody.AppendLine("</body>");
            mailBody.AppendLine("</html>");

            mail.IsBodyHtml = true;
            mail.Body = mailBody.ToString();
            mail.Subject = "Subject";

            Smtp.Send(mail);
        }
    }
}