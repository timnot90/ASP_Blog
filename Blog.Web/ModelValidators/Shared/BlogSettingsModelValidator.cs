using System;
using System.IO;
using System.Net.Sockets;
using Blog.Web.Models.Shared;
using FluentValidation;

namespace Blog.Web.ModelValidators.Shared
{
    public class BlogSettingsModelValidator : AbstractValidator<BlogSettingsModel>
    {
        public BlogSettingsModelValidator()
        {
            RuleFor(model => model.SiteName).NotEmpty().WithMessage("The Sitename cannot be empty.");
            RuleFor(model => model.NumberOfEntriesPerPage)
                .NotEmpty().WithMessage("You have to specify the number of entries per page.");
            RuleFor(model => model.Keywords).Matches(@"[A-Za-z0-9]+(,[A-Za-z0-9\s]*)*[A-Za-z0-9]+").WithMessage("The Keywords do not have a valid format.");
            RuleFor(model => model.RegistrationMailSubject)
                .NotEmpty().WithMessage("You have to specify a subject for the registration mails.");
            RuleFor(model => model.RegistrationMailBody).NotEmpty().WithMessage("You have to specify a body for the registration mails.");
            RuleFor(model => model.RegistrationMailSender).NotEmpty().WithMessage("You have to specify a sender for the registration mails.");
            RuleFor(model => model.PasswordChangeMailSubject).NotEmpty().WithMessage("You have to specify a subject for the password change mails.");
            RuleFor(model => model.PasswordChangeMailBody).NotEmpty().WithMessage("You have to specify a body for the password change mails.");
            RuleFor(model => model.PasswordChangeMailSender).NotEmpty().WithMessage("You have to specify a sender for the password change mails.");
            RuleFor(model => model.WelcomeMailSubject).NotEmpty().WithMessage("You have to specify a subject for the welcome mails.");
            RuleFor(model => model.WelcomeMailBody).NotEmpty().WithMessage("You have to specify a body for the welcome mails.");
            RuleFor(model => model.WelcomeMailSender).NotEmpty().WithMessage("You have to specify a sender for the welcome mails.");
            RuleFor(model => model.FooterText).NotEmpty().WithMessage("You have to specify a footer text for the site.");
            RuleFor(model => model.SmtpServerUrl).NotEmpty().WithMessage("You have to specify the URL of the SMTP server.").Must( BeAValidSmtpConnection ).WithMessage("The specified URL or port of the SMTP server is invalid.");
            RuleFor(model => model.SmtpServerPort).NotEmpty().WithMessage("You have to specify the port of the SMTP server");
            RuleFor(model => model.SmtpServerUsername)
                .Must( BeAValidSmtpUsername ).WithMessage( "You have to specify a username for the SMTP server." );
            RuleFor(model => model.SmtpServerPassword)
                .Must(BeAValidSmtpPassword).WithMessage("You have to specify a password for the SMTP server.");
        }

        private bool BeAValidSmtpUsername(BlogSettingsModel model, string username)
        {
            bool usernameIsValid = model.SmtpAreUsercredentialsMandatoryForLogin &&
                !String.IsNullOrEmpty(username) ||
                !model.SmtpAreUsercredentialsMandatoryForLogin;
            return usernameIsValid;
        }
        private bool BeAValidSmtpPassword(BlogSettingsModel model, string password)
        {
            bool passwordIsValid = model.SmtpAreUsercredentialsMandatoryForLogin
                && !String.IsNullOrEmpty(password) 
                || !model.SmtpAreUsercredentialsMandatoryForLogin;
            return passwordIsValid;
        }
        private static bool BeAValidSmtpConnection(BlogSettingsModel model, string url)
        {
            var valid = false;
            try
            {

                using (var smtpTest = new TcpClient())
                {
                    smtpTest.ReceiveTimeout = 500;
                    IAsyncResult ar = smtpTest.BeginConnect( url, model.SmtpServerPort, null, null );
                    System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                    try
                    {
                        if (!ar.AsyncWaitHandle.WaitOne( TimeSpan.FromSeconds( 5 ), false ))
                        {
                            smtpTest.Close();
                            throw new TimeoutException();
                        }

                        smtpTest.EndConnect( ar );

                        if (smtpTest.Connected)
                        {
                            NetworkStream ns = smtpTest.GetStream();
                            var sr = new StreamReader( ns );
                            if (sr.ReadLine().Contains( "220" ))
                            {
                                valid = true;
                            }
                            smtpTest.Close();
                        }
                    }
                    finally
                    {
                        wh.Close();
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
                // suppress any errors
            }

            return valid;
        }
    }
}