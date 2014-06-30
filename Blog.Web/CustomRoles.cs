using System;
using System.Web.Security;

namespace Blog.Web
{
    public static class CustomRoles
    {
        public const string Administrator = "Administrator";
        public const string User = "User";

        public static bool IsUserInRole( string roleName  )
        {
            try
            {
                return Roles.IsUserInRole(roleName);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}