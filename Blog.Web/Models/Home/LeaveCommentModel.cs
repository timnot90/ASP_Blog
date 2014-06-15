using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class LeaveCommentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int BlogentryId { get; set; }
        public string Header { get; set; }

        [Required(ErrorMessage = "Your comment needs a body.")]
        public string Body { get; set; }

        public string CaptchaResult { get; set; }

        public LeaveCommentModel()
        {

        }

        public LeaveCommentModel(Comment source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(Comment source)
        {
            Id = source.ID;
            BlogentryId = source.BlogentryID;
            Header = source.Header;
            Body = source.Body;
        }

        public void UpdateSource(Comment source)
        {
            source.BlogentryID = BlogentryId;
            source.Header = Header;
            source.Body = Body;
        }
    }
}