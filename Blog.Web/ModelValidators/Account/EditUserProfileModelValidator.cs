using Blog.Web.Models.Account;
using FluentValidation;

namespace Blog.Web.ModelValidators.Account
{
    public class EditUserProfileModelValidator : AbstractValidator<EditUserProfileModel>
    {
        public EditUserProfileModelValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty().WithMessage("An error occured while validatiing your " +
                                         "model. Please Reload the page and try again.");
            RuleFor(model => model.DisplayName)
                .NotEmpty().WithMessage("A Displayname has to be declared.");
            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("An E-Mail adress must be declared.")
                .EmailAddress().WithMessage("The E-Mail Adress is not valid.")
                .Length(5, 100).WithMessage("The length of the email address is " +
                                              "not supported. It has has to have " +
                                              "between 5 nd 100 characters.");
            RuleFor(model => model.Street)
                .Length( 1, 100 ).WithMessage("The length of your street-name is too " +
                                              "long. It has to have between 1 and " +
                                              "100 characters.");
            RuleFor(model => model.HouseNumber)
                .Length(1, 10).WithMessage("The length of your house number is too " +
                                              "long. It has to have between 1 and " +
                                              "10 characters.");
            RuleFor(model => model.Town)
                .Length(1, 50).WithMessage("The length of your town name is too " +
                                              "long. It has to have between 1 and " +
                                              "50 characters.");
            RuleFor(model => model.Zip)
                .Length(1, 10).WithMessage("The length of your zip is too " +
                                              "long. It has to have between 1 and " +
                                              "10 characters.");
            RuleFor(model => model.Country)
                .Length(1, 50).WithMessage("The length of your country name is too " +
                                              "long. It has to have between 1 and " +
                                              "50 characters.");
            RuleFor(model => model.Country)
                .Length(1, 50).WithMessage("The length of your country name is too " +
                                              "long. It has to have between 1 and " +
                                              "50 characters.");
            RuleFor(model => model.Forename)
                .Length(1, 50).WithMessage("The length of your forename is too " +
                                              "long. It has to have between 1 and " +
                                              "50 characters.");
            RuleFor(model => model.LastName)
                .Length(1, 50).WithMessage("The length of your lastname is too " +
                                              "long. It has to have between 1 and " +
                                              "50 characters.");
        }
    }
}