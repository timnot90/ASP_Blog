using System.Text.RegularExpressions;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Home
{
    [Validator(typeof(LeaveCommentModelValidator))]
    public class LeaveCommentModel
    {
        public int BlogentryId { get; set; }
        public string Header { get; set; }

        public string Body { get; set; }

        public string CaptchaResult { get; set; }

        public void UpdateSource(Comment source)
        {
            source.BlogentryID = BlogentryId;
            source.Header = FilterHtmlTags(Header);
            source.Body = FilterHtmlTags(Body);
        }

        private string FilterHtmlTags(string text)
        {
            if (text == null) return null;

            Regex replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            Regex removeHtml = new Regex(@"<[^>]*>");
            Regex replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }
    }
}