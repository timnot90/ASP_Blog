using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Blog.Web.Controllers;
using WebMatrix.WebData;

namespace Blog.Web
{
    public static class CustomRoles
    {
        public const string Administrator = "Administrator";
        public const string User = "User";

        public static bool IsUserInRole( string role )
        {
            try
            {
                return Roles.IsUserInRole( role );
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }
    }
}