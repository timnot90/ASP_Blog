using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        private string[] UserProfilesRequired { get; set; }

        public CustomAuthorize(params object[] userProfilesRequired)
        {
            if (userProfilesRequired.Any(p => p.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("userProfilesRequired");

            UserProfilesRequired = userProfilesRequired.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;

            foreach (var role in UserProfilesRequired)
                if (HttpContext.Current.User.IsInRole(role))
                    authorized = true;

            if (!authorized)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Login", "Account", new { Area = "" });
                context.Result = new RedirectResult(logonUrl);
            }
        }
    }
}
