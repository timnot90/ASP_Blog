using Blog.Web.Areas.Administration.Models.Home;
using FluentValidation;

namespace Blog.Web.Areas.Administration.ModelValidators
{
    public class EditBlogentryModelValidator : AbstractValidator<EditBlogentryModel>
    {
        public EditBlogentryModelValidator()
        {
            RuleFor(model => model.Id).NotEmpty().WithMessage("There was an error while validating your " +
                                                              "input. Please reload the page and try again.");
            RuleFor(model => model.Header).NotEmpty().WithMessage("The header cannot be empty.");
            RuleFor(model => model.Body).NotEmpty().WithMessage("The body cannot be empty.");
        }
    }
}