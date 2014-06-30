using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class BlogentryListItemModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<CategoryModel> Categories { get; private set; } 
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate);
            }
        }
        public BlogentryCreatorModel Creator { get; set; }

        public BlogentryListItemModel()
        {
        }

        public BlogentryListItemModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.BodyWithBr;
            CreationDate = source.CreationDate;
            Categories = source.Categories.Select(c => new CategoryModel(c)).ToList();
            Creator = new BlogentryCreatorModel(source.UserProfile);
        }
    }
}