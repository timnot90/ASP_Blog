using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Annotations;
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
        public List<CommentModel> Comments { get; set; }
        public UserModel Creator { get; set; }
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate);
            }
        }

        [UsedImplicitly]
        public BlogentryDetailModel()
        {
        }

        public BlogentryDetailModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.BodyWithBr;
            CreationDate = source.CreationDate;
            Creator = new UserModel(source.UserProfile);
            Comments = source.Comments.Select(m => new CommentModel(m)).ToList();
        }
    }
}