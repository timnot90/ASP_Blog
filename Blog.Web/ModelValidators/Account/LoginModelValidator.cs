using Blog.Web.Models.Account;
using FluentValidation;

namespace Blog.Web.ModelValidators.Account
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("The username cannot be empty.");
            RuleFor(model => model.Password).NotEmpty().WithMessage("The password cannot be empty.");
        }
    }
}