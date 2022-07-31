using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddToActivityListCommandValidator : AbstractValidator<AddToActivityListCommand>
    {
        public AddToActivityListCommandValidator()
        {
            RuleFor(c => c.Person.Email)
                .NotEmpty().WithMessage("Person email address must not be empty.")
                .EmailAddress().WithMessage("Person email address is invalid.");
            RuleFor(c => c.Person.FirstName)
                .NotEmpty().WithMessage("First name must not be empty")
                .Matches(@"^[a-zA-Z*\s.,-]*$").WithMessage("First name contains an invalid character")
                .MaximumLength(50).WithMessage("First name must not be long than 100 characters");
            RuleFor(c => c.Person.LastName)
                .NotEmpty().WithMessage("Last name must not be empty")
                .Matches(@"^[a-zA-Z*\s.,-]*$").WithMessage("Last name contains an invalid character")
                .MaximumLength(50).WithMessage("Last name must not be long than 100 characters");
            RuleFor(c => c.Person.Comments)
                .Matches(@"^[a-zA-Z0-9\s.,'!*-]*$").WithMessage("Comment contains an invalid character")
                .MaximumLength(100).WithMessage("Comment must not be long than 500 characters");
            RuleFor(c => c.Person.ActivityId)
                .NotEmpty().WithMessage("Activity Id must not be empty.");
            RuleFor(c => c.Person.Gender)
                .NotEmpty().WithMessage("Gender must not be empty.")
                .Must(p => p == "Male" || p == "Female").WithMessage("Gender selection must be Male or Female respectively");
                

        }   
    }
}