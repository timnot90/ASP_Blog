using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Areas.Administration.ModelValidators;
using FluentValidation.Attributes;

namespace Blog.Web.Areas.Administration.Models.Home
{
    [Validator(typeof(AddBlogentryModelValidator))]
    public class AddBlogentryModel
    {
        private List<CategorySelectedModel> _categories = new List<CategorySelectedModel>();

        [AllowHtml]
        public string Header { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public List<CategorySelectedModel> Categories
        {
            get { return _categories ?? (_categories = new List<CategorySelectedModel>()); }
            set { _categories = value; }
        }

        public void UpdateSource( Blogentry source )
        {
            source.Header = FilterHtmlTags(Header);
            source.BodyWithLinebreaks = Body;
            source.BodyWithBr = FilterHtmlTags(Body);
        }

        private string FilterHtmlTags(string text)
        {
            if (text == null) return null;

            var replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            var removeHtml = new Regex(@"<[^>]*>");
            var replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }
    }
}