using Blog.Web.Models.Account;
using FluentValidation;

namespace Blog.Web.ModelValidators.Account
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("The username cannot be empty.");
            RuleFor(model => model.DisplayName).NotEmpty().WithMessage("The displayname cannot be empty.");
            RuleFor(model => model.Password).NotEmpty().WithMessage("The password cannot be empty.")
                .Equal(model => model.PasswordConfirmed).WithMessage("Your password do not correspond.");
            RuleFor(model => model.Email).NotEmpty().WithMessage("The username cannot be empty.")
                .EmailAddress().WithMessage( "The format of your email address is not valid." );
        }
    }
}