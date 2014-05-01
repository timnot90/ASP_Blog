﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id=UrlParameter.Optional},
                namespaces: new[] { "Blog.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Blog",
                url: "Home/Index/{categoryId}/{monthAndYear}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, monthAndYear = UrlParameter.Optional },
                namespaces: new[] { "Blog.Web.Controllers" }
            );
        }
    }
}