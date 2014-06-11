using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Home
{
    public class BlogentryDetailModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool CommentsActivated { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<CommentModel> Comments { get; set; }
        public UserProfileModel Creator { get; set; }
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate);
            }
        }

        public BlogentryDetailModel()
        {
        }

        public BlogentryDetailModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.Body;
            CreationDate = source.CreationDate;
            Creator = new UserProfileModel(source.UserProfile);
            Comments = source.Comments.Select(m => new CommentModel(m)).ToList();
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = Header;
            source.Body = Body;
        }
    }
}