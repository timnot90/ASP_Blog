using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Blog
{
    public class BlogEntryListItemModel
    {
        public int ID { get; set; }
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
        public UserProfileModel Creator { get; set; }

        public BlogEntryListItemModel()
        {
        }

        public BlogEntryListItemModel(Blogentry blogentry)
        {
            this.UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            this.ID = source.ID;
            this.Header = source.Header;
            this.Body = source.Body;
            this.CreationDate = source.CreationDate;
            this.Categories = source.Categories.Select(c => new CategoryModel(c)).ToList();
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = this.Header;
            source.Body = this.Body;
            source.CreationDate = this.CreationDate;
        }
    }
}