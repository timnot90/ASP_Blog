using System;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Home
{
    public class CommentModel
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public UserProfileModel Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, at {0:t}", CreationDate);
            }
        }

        public CommentModel()
        {
        }

        public CommentModel(Comment source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(Comment source)
        {
            Header = source.Header;
            Body = source.Body;
            Creator = new UserProfileModel(source.UserProfile);
            CreationDate = source.CreationDate;
        }
    }
}