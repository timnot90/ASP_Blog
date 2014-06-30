using System.Collections.Generic;
using System.Text.RegularExpressions;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Areas.Administration.Models
{
    [Validator(typeof(EditBlogentryModelValidator))]
    public class EditBlogentryModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<CategoryModel> Categories { get; set; }

        public EditBlogentryModel()
        {
        }

        public EditBlogentryModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.BodyWithLinebreaks;
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = FilterHtmlTags(Header);
            source.BodyWithLinebreaks = Body;
            source.BodyWithBr = FilterHtmlTags( Body );
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