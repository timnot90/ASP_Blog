using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        private const string NavigationbarLinkBaseString =
            "<li><a href='{0}'><i class='glyphicon {1}'></i> {2}</a></li>";

        private const string NavigationbarDropdownBaseString =
            "<a href='#' class='dropdown-toggle' data-toggle='dropdown'><i class='glyphicon {0}'></i> {1} <b class='caret'></b></a>";

        public static MvcHtmlString NavigationbarLink(this UrlHelper helper, string actionName, string controllerName, string linkText, string bootstrapIconClass, string areaName = "")
        {
            var returnValue = new MvcHtmlString(string.Format(NavigationbarLinkBaseString, helper.Action(actionName, controllerName, new { area = areaName }), bootstrapIconClass, linkText));

            return returnValue;
        }
        public static MvcHtmlString NavigationbarDropdownLink(this UrlHelper helper, string linkText, string bootstrapIconClass)
        {
            var returnValue = new MvcHtmlString(string.Format(NavigationbarDropdownBaseString, bootstrapIconClass, linkText));

            return returnValue;
        }
    }
}
