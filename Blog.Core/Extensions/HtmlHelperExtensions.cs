using System;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        private const string UserProfileTextBoxBaseString =
            "<div class='form-group'>" +
            "<label class='required' for='{0}'>{1}</label>" +
            "<div>{2}</div>" +
            "</div>";

        private const string UserProfilePasswordBaseString =
            "<div class='form-group'>" +
            "<label class='required' for='{0}'>{1}</label>" +
            "<div>{2}</div>" +
            "</div>";


        public static MvcHtmlString UserProfileTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, bool isRequired = false)
        {
            var returnValue = new MvcHtmlString(string.Format(UserProfileTextBoxBaseString,
                helper.IdFor(expression),
                helper.DisplayNameFor(expression),
                helper.TextBoxFor(expression, new { @class = "form-control" + (isRequired ? " required" : ""), placeholder = helper.DisplayNameFor(expression) })));

            return returnValue;
        }

        public static MvcHtmlString UserProfilePasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var returnValue = new MvcHtmlString(string.Format(UserProfilePasswordBaseString,
                helper.IdFor(expression),
                helper.DisplayNameFor(expression),
                helper.PasswordFor(expression, new { @class = "form-control required", placeholder = helper.DisplayNameFor(expression) })));

            return returnValue;
        }
    }
}