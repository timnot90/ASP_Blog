using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Web.ModelValidators.Home;
using FluentValidation.Attributes;

namespace Blog.Web.Models.Home
{
    [Validator(typeof(AddBlogentryModelValidator))]
    public class AddBlogentryModel
    {
        private List<CategoryModel> _categories = new List<CategoryModel>();

        public string Header { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public List<CategoryModel> Categories
        {
            get { return _categories ?? (_categories = new List<CategoryModel>()); }
            set { _categories = value; }
        }
    }
}