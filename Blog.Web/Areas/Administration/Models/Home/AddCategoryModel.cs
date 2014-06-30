using Blog.Core.DataAccess.Blog;
using Blog.Web.Areas.Administration.ModelValidators;
using FluentValidation.Attributes;

namespace Blog.Web.Areas.Administration.Models.Home
{
    [Validator(typeof(AddCategoryModelValidator))]
    public class AddCategoryModel
    {
        public string Name { get; set; }

        public void UpdateSource(Category source)
        {
            source.Name = Name;
        }
    }
}