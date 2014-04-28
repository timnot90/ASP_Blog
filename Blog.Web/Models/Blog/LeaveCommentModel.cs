using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Blog
{
    public class LeaveCommentModel
    {
        public int Id { get; set; }
        public int BlogentryId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }

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