using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Home
{
    [Validator(typeof(EditBlogentryModelValidator))]
    public class EditBlogentryModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<CategoryModel> Categories { get; set; }

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