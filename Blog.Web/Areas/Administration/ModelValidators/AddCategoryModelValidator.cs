using Blog.Web.Areas.Administration.Models.Home;
using FluentValidation;

namespace Blog.Web.Areas.Administration.ModelValidators
{
    public class AddCategoryModelValidator : AbstractValidator<AddCategoryModel>
    {
        public AddCategoryModelValidator()
        {
            RuleFor( model => model.Name ).NotEmpty().WithMessage( "The Name of the category cannot be empty." );
        }
    }
}