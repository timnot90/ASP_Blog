using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class AddBlogentryModel
    {
        private List<CategoryModel> _categories = new List<CategoryModel>();
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }

        [AllowHtml]
        [Required]
        public string Body { get; set; }

        public List<CategoryModel> Categories
        {
            get { return _categories ?? (_categories = new List<CategoryModel>()); }
            set { _categories = value; }
        }

        public AddBlogentryModel()
        {
        }

        public AddBlogentryModel(Blogentry source)
        {
            UpdateModel(source);
        }

        private void UpdateModel(Blogentry source)
        {
            Id = source.ID;
            Header = source.Header;
            Body = source.Body;
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = Header;
            source.Body = Body;
        }
    }
}