using Blog.Web.Areas.Administration.Models.Home;
using FluentValidation;

namespace Blog.Web.Areas.Administration.ModelValidators
{
    public class AddBlogentryModelValidator : AbstractValidator<AddBlogentryModel>
    {
        public AddBlogentryModelValidator()
        {
            RuleFor(model => model.Header).NotEmpty().WithMessage("Please specify a header for your entry.");
            RuleFor(model => model.Body).NotEmpty().WithMessage("Please specify a body for your entry.");
        }
    }
}