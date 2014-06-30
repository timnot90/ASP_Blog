using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RouteConfig
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute( "Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", area = "", id = UrlParameter.Optional},
                new[] {"Blog.Web.Controllers"}
                );

            routes.MapRoute( "Blog", "Home/Index/{categoryId}/{monthAndYear}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    monthAndYear = UrlParameter.Optional
                }, new[] {"Blog.Web.Controllers"}
                );
        }
    }
}