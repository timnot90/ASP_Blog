using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Blog.Web
{
    public static class MailSender
    {
        private static readonly SmtpClient smtp = new SmtpClient();

        public static void SendAccountValidationToken(string token, string recipient)
        {
            MailMessage mail = new MailMessage();
            string linkText = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Account/ValidateUser?token=" + token;

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

            smtp.Send(mail);
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

            smtp.Send(mail);
        }
    }
}