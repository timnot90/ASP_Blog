using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString UserProfileTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isRequired = false, bool isPassword = false)
        {
            TagBuilder outerTag = CreateOuterTag();

            string innerHtml = string.Empty;
            innerHtml += helper.CreateLabel(expression);

            TagBuilder textBoxEnvelope = CreateInputEnvelope();
            if (!isPassword)
            {
                textBoxEnvelope.InnerHtml += helper.TextBoxFor(expression,
                    new { @class = "form-control required", @placeholder = helper.DisplayNameFor(expression) });
            }
            else
            {
                textBoxEnvelope.InnerHtml += helper.PasswordFor(expression,
                       new { @class = "form-control required", @placeholder = helper.DisplayNameFor(expression) });
            }
            innerHtml += textBoxEnvelope.ToString();

            outerTag.InnerHtml += innerHtml;

            return new MvcHtmlString(outerTag.ToString());
        }

        public static string BlogEntryListItemBodyShort(this HtmlHelper helper, string text)
        {
            return text.Length <= 500 ? text : text.Substring(0, 500) + " ..";
        }

        private static MvcHtmlString CreateLabel<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.LabelFor(expression, new { @class = "required" });
        }

        private static TagBuilder CreateOuterTag()
        {
            TagBuilder outerTag = new TagBuilder("div");
            outerTag.MergeAttribute("class", "form-group");
            return outerTag;
        }

        private static TagBuilder CreateInputEnvelope()
        {
            TagBuilder inputEnvelope = new TagBuilder("div");
//            inputEnvelope.MergeAttribute("class", "col-lg-10");
            return inputEnvelope;
        }
    }
}
