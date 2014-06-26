using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Shared;

namespace Blog.Web.Areas.Administration.Services
{
    public class BlogAdministrationService : IBlogAdministrationService
    {
        #region variables

        private readonly IBlogRepository _repository = new BlogRepository();

        #endregion

        #region Users

        public UserListModel GetUserListModel()
        {
            UserListModel model = new UserListModel
            {
                Users = _repository.GetAllUserProfiles().Select( e => new UserListItemModel( e ) ).ToList()
            };

            foreach (UserListItemModel user in model.Users)
            {
                user.Roles =
                    Roles.GetAllRoles()
                        .Select( r => new RoleModel( r, Roles.GetRolesForUser( user.UserName ).Contains( r ) ) )
                        .ToList();
            }
            return model;
        }

        public void SetUserLockedState( int userId, bool state )
        {
            _repository.SetUserLockedSate( userId, state );
        }

        public void ChangeRole( int id, string newRole, bool added )
        {
            try
            {
                string username = _repository.GetUserProfileById( id ).UserName;
                if (added)
                {
                    Roles.AddUserToRole( username, newRole );
                }
                else
                {
                    Roles.RemoveUserFromRole( username, newRole );
                }
            }
            catch (ArgumentNullException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (ProviderException)
            {
            }
        }

        #endregion

        #region Blogsettings

        public BlogSettingsModel GetBlogSettings()
        {
            return new BlogSettingsModel( _repository.GetBlogSettings() );
        }

        public void StoreSettings( BlogSettingsModel model )
        {
            bool isSmtpServerAddressValid = ValidSMTP( model.SmtpServerUrl, model.SmtpServerPort );
            bool isSmtpUsernameValid = model.SmtpAreUsercredentialsMandatoryForLogin && !String.IsNullOrEmpty(model.SmtpServerUsername) || !model.SmtpAreUsercredentialsMandatoryForLogin;
            bool isSmtpPasswordValid = model.SmtpAreUsercredentialsMandatoryForLogin && !String.IsNullOrEmpty(model.SmtpServerPassword) || !model.SmtpAreUsercredentialsMandatoryForLogin;

            var errors = new Dictionary<string, string>();

            if (!isSmtpServerAddressValid)
            {
                errors.Add( "InvalidSmtpAddress", "The smtp-server address is not valid." );
            }
            if (!isSmtpUsernameValid)
            {
                errors.Add("InvalidSmtpUsername", "The smtp-server username is not valid.");
            }
            if (!isSmtpPasswordValid)
            {
                errors.Add("InvalidSmtpPassword", "The smtp-server password is not valid.");
            }

            if (errors.Count == 0)
            {
                Setting setting = _repository.GetBlogSettings();
                model.UpdateSource( setting );
                _repository.StoreSettings( setting );
            }
            else
            {
                throw new SmtpInvalidException( errors );
            }
        }

        public void ChangeSmtpPassword(ChangeSmtpPasswordModel model)
        {
            if (model.NewPassword.Equals(model.NewPasswordConfirmed))
            {
                Setting blogSettings = _repository.GetBlogSettings();
                if (blogSettings.SmtpServerPassword == null || blogSettings.SmtpServerPassword.Equals(model.CurrentPassword))
                {
                    blogSettings.SmtpServerPassword = model.NewPassword;
                    _repository.StoreSettings(blogSettings);
                }
                else
                {
                    throw new CurrentPasswordInvalidException();
                }
            }
            else
            {
                throw new NewPasswordInvalidException();
            }
        }

        private bool ValidSMTP( string hostName, int port )
        {
            bool valid = false;
            try
            {

                using (TcpClient smtpTest = new TcpClient())
                {
                    smtpTest.ReceiveTimeout = 500;
                    IAsyncResult ar = smtpTest.BeginConnect(hostName, port, null, null);
                    System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                    try
                    {
                        if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5), false))
                        {
                            smtpTest.Close();
                            throw new TimeoutException();
                        }

                        smtpTest.EndConnect(ar);

                        if (smtpTest.Connected)
                        {
                            NetworkStream ns = smtpTest.GetStream();
                            StreamReader sr = new StreamReader(ns);
                            if (sr.ReadLine().Contains("220"))
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
            // suppress any errors
            catch (Exception) { }

            return valid;
        }

        #endregion
    }
}