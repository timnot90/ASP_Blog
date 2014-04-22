using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Blog
{
    public class AddBlogentryModel
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<CategoryModel> AvailableCategories { get; set; }

        public AddBlogentryModel()
        {
        }

        public AddBlogentryModel(Blogentry source)
        {
            UpdateModel(source);
        }

        private void UpdateModel(Blogentry source)
        {
            this.ID = source.ID;
            this.Header = source.Header;
            this.Body = source.Body;
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = this.Header;
            source.Body = this.Body;
        }
    }
}