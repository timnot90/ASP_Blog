using System.Net.Mail;
using System.Security.Policy;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Blog.Web
{
    public static class MailSender
    {
        private static readonly SmtpClient smtp = new SmtpClient();

        public static void SendToken( string token, string recipient )
        {
            MailMessage mail = new MailMessage();
            string link = "";
            link += HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Account/ValidateUser?token=" + token;
            
            mail.To.Add( new MailAddress( recipient ) );
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = false;
            mail.Body = link;
            mail.Subject = "Subject";

            smtp.Send( mail );
        }
    }
}