using System;
using System.Web.Mvc;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int BlogentryId { get; set; }
        public string Header { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public CommentCreatorModel Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, at {0:t}", CreationDate);
            }
        }

        [UsedImplicitly]
        public CommentModel()
        {
        }

        public CommentModel(Comment source)
        {
            UpdateModel(source);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateModel(Comment source)
        {
            Id = source.ID;
            BlogentryId = source.BlogentryID;
            Header = source.Header;
            Body = source.Body;
            Creator = new CommentCreatorModel(source.UserProfile);
            CreationDate = source.CreationDate;
        }
    }
}