using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Home
{
    public class EditBlogentryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="The header of your blogentry mustn't be empty.")]
        public string Header { get; set; }
        [Required(ErrorMessage = "The body of your blogentry mustn't be empty.")]
        public string Body { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public bool SuccessfullySaved { get; set; }

        public EditBlogentryModel()
        {
        }

        public EditBlogentryModel(Blogentry blogentry)
        {
            UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
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