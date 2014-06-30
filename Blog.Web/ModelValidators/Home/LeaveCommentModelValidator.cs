using Blog.Core.Extensions;
using Blog.Web.Models.Home;
using FluentValidation;
using WebMatrix.WebData;

namespace Blog.Web.ModelValidators.Home
{
    public class LeaveCommentModelValidator : AbstractValidator<LeaveCommentModel>
    {
        public LeaveCommentModelValidator()
        {
            RuleFor( model => model.BlogentryId )
                .NotEmpty().WithMessage("There was an error while validating your " +
                                        "input. Please reload the page and try again.");
            RuleFor( model => model.Body ).NotEmpty().WithMessage( "Please specify a body for your comment" );
            RuleFor( model => model.CaptchaResult ).Must( BeAValidCaptcha ).WithMessage("The entered value for the captcha is invalid.");
        }

        private bool BeAValidCaptcha( string captcha )
        {
            return WebSecurity.IsAuthenticated || !WebSecurity.IsAuthenticated && CaptchaHelper.ValidateCaptchaResult( captcha );
        }
    }
}