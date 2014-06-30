using System.Collections.Generic;
using System.Text.RegularExpressions;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Areas.Administration.ModelValidators;
using FluentValidation.Attributes;

namespace Blog.Web.Areas.Administration.Models.Home
{
    [Validator(typeof(EditBlogentryModelValidator))]
    public class EditBlogentryModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<CategorySelectedModel> Categories { get; set; }


        [UsedImplicitly]
        public EditBlogentryModel()
        {
        }

        public EditBlogentryModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        // ReSharper disable once MemberCanBePrivate.Global
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

            var replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            var removeHtml = new Regex(@"<[^>]*>");
            var replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(
                removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }
    }
}