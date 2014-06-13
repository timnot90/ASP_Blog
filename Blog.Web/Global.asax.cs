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

            if (new BlogSharedService().GetNumberOfUsers() == 0)
            {
                WebSecurity.CreateUserAndAccount("admin", "1",
                    new
                    {
                        UserNameLowercase = "admin",
                        DisplayName = "admin",
                        EMail = "timonotheisen@googlemail.com",
                        EmailLowercase = "timonotheisen@googlemail.com",
                        IsLocked = false
                    });
                Roles.CreateRole(CustomRoles.Administrator);
                Roles.CreateRole(CustomRoles.User);
                Roles.AddUserToRole("admin", CustomRoles.Administrator);
            }
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}