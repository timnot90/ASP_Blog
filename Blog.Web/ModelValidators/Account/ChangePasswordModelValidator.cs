using Blog.Web.Models.Account;
using FluentValidation;

namespace Blog.Web.ModelValidators.Account
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor( changePasswordModel => changePasswordModel.CurrentPassword ).NotEmpty().WithMessage("The current password cannot be empty.");
            RuleFor( changePasswordModel => changePasswordModel.NewPassword )
                .NotEmpty().WithMessage("The new password mustn't be empty.")
                .Equal( c => c.NewPasswordConfirmed )
                .WithMessage( "The given passwords do not correspond." );
        }
    }
}