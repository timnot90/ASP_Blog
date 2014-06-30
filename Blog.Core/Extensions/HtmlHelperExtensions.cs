using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        private const string UserProfileTextBoxBaseString =
            "<div class='form-group'>" +
            "<label for='{0}'>{1}</label>" +
            "<div>{2}</div>" +
            "</div>";

        private const string UserProfilePasswordBaseString =
            "<div class='form-group'>" +
            "<label for='{0}'>{1}</label>" +
            "<div>{2}</div>" +
            "</div>";

        private const string CustomValidationSummaryBaseString = "<div class='panel panel-danger validation-summary'>" + 
                "<div class='panel-heading'>Error</div>" + 
                "<div class='panel-body'>" + 
                "{0}" + 
                "</div>" + 
                "</div>";

        private const string PanelBaseString = "<div class='panel {0}'><div class='panel-heading'>{1}</div><div class='panel-body'>{2}</div></div>";

        public static MvcHtmlString TextBoxWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var returnValue = new MvcHtmlString(string.Format(UserProfileTextBoxBaseString,
                helper.IdFor(expression),
                helper.DisplayNameFor(expression),
                helper.TextBoxFor(expression, new { @class = "form-control", placeholder = helper.DisplayNameFor(expression)})));

            return returnValue;
        }

        public static MvcHtmlString PasswordWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var returnValue = new MvcHtmlString(string.Format(UserProfilePasswordBaseString,
                helper.IdFor(expression),
                helper.DisplayNameFor(expression),
                helper.PasswordFor(expression, new { @class = "form-control", placeholder = helper.DisplayNameFor(expression)})));

            return returnValue;
        }

        // ReSharper disable once UnusedParameter.Global
        public static MvcHtmlString Panel(this HtmlHelper helper, string panelClass, string heading, string body)
        {
            var returnValue = new MvcHtmlString(string.Format(PanelBaseString, panelClass, heading, body));

            return returnValue;
        }

        public static MvcHtmlString CustomValidationSummary(this HtmlHelper helper)
        {
            var returnValue = helper.ViewData.ModelState.IsValid ? new MvcHtmlString( "" ) :
                new MvcHtmlString(string.Format(CustomValidationSummaryBaseString, helper.ValidationSummary()));
            return returnValue;
        }
    }
}