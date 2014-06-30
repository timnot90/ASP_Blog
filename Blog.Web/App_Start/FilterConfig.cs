using System.Web.Mvc;

namespace Blog.Web
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}