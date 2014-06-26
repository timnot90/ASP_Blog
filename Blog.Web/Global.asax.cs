using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Blog.Web.Areas.Administration.Services;
using Blog.Web.Services.Account;
using Blog.Web.Services.Shared;
using WebMatrix.WebData;

namespace Blog.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfiles", "ID", "UserName", true);

            // when the blog is started for the first time, the roles and the admin are created 
            if (new BlogSharedService().GetNumberOfAdministrators() == 0)
            {
                try
                {
                    Roles.CreateRole( CustomRoles.Administrator );
                    Roles.CreateRole( CustomRoles.User );

                    WebSecurity.CreateUserAndAccount( "admin", "1",
                        new
                        {
                            UserNameLowercase = "admin",
                            DisplayName = "admin",
                            EMail = "timonotheisen@googlemail.com",
                            EmailLowercase = "timonotheisen@googlemail.com",
                            IsLocked = false
                        } );
                    Roles.AddUserToRole( "admin", CustomRoles.Administrator );
                }
                catch (Exception)
                {
                    // ignore all exceptions
                }
            }
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}