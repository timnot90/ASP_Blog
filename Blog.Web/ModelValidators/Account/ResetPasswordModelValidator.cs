using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Web.Models.Account;
using FluentValidation;

namespace Blog.Web.ModelValidators.Account
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor( model => model.Email )
                .NotEmpty().WithMessage( "Please enter your email-address" )
                .EmailAddress().WithMessage( "The format of the entered email-address is not valid." );
        }
    }
}