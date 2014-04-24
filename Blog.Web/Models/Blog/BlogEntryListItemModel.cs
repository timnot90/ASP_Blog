using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Blog
{
    public class BlogEntryListItemModel
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
        public UserProfileModel Creator { get; set; }

        public BlogEntryListItemModel()
        {
        }

        public BlogEntryListItemModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.Body;
            CreationDate = source.CreationDate;
            Categories = source.Categories.Select(c => new CategoryModel(c)).ToList();
            this.Creator = new UserProfileModel(source.UserProfile);
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = Header;
            source.Body = Body;
            source.CreationDate = CreationDate;
        }
    }
}