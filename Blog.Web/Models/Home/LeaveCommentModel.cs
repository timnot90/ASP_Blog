using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Home
{
    //[Validator(typeof(LeaveCommentModelValidator))]
    public class LeaveCommentModel
    {
        [Required(ErrorMessage = "There was an error while validating your input. Please reload the page and try again.")]
        public int BlogentryId { get; set; }

        [AllowHtml]
        [StringLength(100, ErrorMessage = "Header cannot be longer than 100 characters.")]
        public string Header { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter th Body of your coment.")]
        [StringLength(1000, ErrorMessage = "Header cannot be longer than 1000 characters.")]
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