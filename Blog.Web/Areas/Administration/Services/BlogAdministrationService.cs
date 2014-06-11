using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web;
using System.Web.Security;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Repositories;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Account;
using WebMatrix.WebData;

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
            var model = new UserListModel
            {
                Users = _repository.GetAllUserProfiles().Select(e => new UserListItemModel(e)).ToList()
            };

            foreach(UserListItemModel user in model.Users)
            {
                user.Roles = Roles.GetAllRoles().Select(r => new RoleModel(r, Roles.GetRolesForUser(user.UserName).Contains(r))).ToList();
            }
            return model;
        }

        public void SetUserLockedState(int userId, bool state)
        {
            _repository.SetUserLockedSate( userId, state );
        }

        public void ChangeRole( int id, string newRole, bool added )
        {
            try
            {
                string username = _repository.GetUserProfile( id ).UserName;
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

        #region Settings

        public BlogSettingsModel GetBlogSettings()
        {
            return  new BlogSettingsModel(_repository.GetBlogSettings());
        }

        public void StoreSettings(BlogSettingsModel model)
        {
            if (ValidSMTP( model.SmtpServerAddress ))
            {
                Setting setting = _repository.GetBlogSettings();
                model.UpdateSource( setting );
                _repository.StoreSettings( setting );
            }
            else
            {
                throw new SmtpException();
            }
        }

        private bool ValidSMTP(string hostName)
        {
            bool valid = false;
            try
            {
                TcpClient smtpTest = new TcpClient();
                smtpTest.Connect(hostName, 587);
                smtpTest.ReceiveTimeout = 500;
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
            catch
            {

            }
            return valid;
        }
        #endregion
    }
}