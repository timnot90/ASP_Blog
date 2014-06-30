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
            source.Header = Header;
            source.Body = Body;
        }
    }
}