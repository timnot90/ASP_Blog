using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Areas.Administration.Models
{
    [Validator(typeof(AddBlogentryModelValidator))]
    public class AddBlogentryModel
    {
        private List<CategoryModel> _categories = new List<CategoryModel>();

        public string Header { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public List<CategoryModel> Categories
        {
            get { return _categories ?? (_categories = new List<CategoryModel>()); }
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

            Regex replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            Regex removeHtml = new Regex(@"<[^>]*>");
            Regex replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }
    }
}