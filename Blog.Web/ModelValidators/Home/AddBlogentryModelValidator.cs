using Blog.Web.Areas.Administration.Models;
using Blog.Web.Models.Home;
using FluentValidation;

namespace Blog.Web.ModelValidators.Home
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