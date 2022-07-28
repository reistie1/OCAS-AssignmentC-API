using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class UpdateSchoolInformationCommandValidator : AbstractValidator<UpdateSchoolInformationCommand>
    {
        public UpdateSchoolInformationCommandValidator()
        {
            RuleFor(c => c.School.Name)                
                .NotEmpty().WithMessage("Course name must not be empty")
                .MaximumLength(100).WithMessage("Course name must not be greater than length of 100 characters")
                .Matches(@"^[A-Za-z0-9\s-_*]+$").WithMessage("Course name contains an invalid character.");
            RuleFor(v => v.School.Address)
                .ChildRules(c => c.RuleFor(a => a.Address1)
                    .NotEmpty().WithMessage("School primary address must not be empty")
                    .Matches(@"^[a-zA-Z0-9\s.,#-_']+$").WithMessage("Primary address contains an invalid character.")
                    .MaximumLength(150).WithMessage("Address is too long, max length is 150"))
                .ChildRules(c => c.RuleFor(a => a.City)
                    .NotEmpty().WithMessage("School city must not be empty")
                    .Matches(@"([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$").WithMessage("City contains an invalid character.")
                    .MaximumLength(90).WithMessage("City has max length of 90"))
                .ChildRules(c => c.RuleFor(a => a.Province)
                    .NotEmpty().WithMessage("School province must not be empty")
                    .Matches(@"^[A-Za-z\s-_.,']+$").WithMessage("Province contains an invalid character.")
                    .MaximumLength(120).WithMessage("Address is too long, max length is 120"))
                .ChildRules(c => c.RuleFor(a => a.PostalCode)
                    .NotEmpty().WithMessage("School postal code must not be empty")
                    .MaximumLength(120).WithMessage("Address is too long, max length is 120"))
                .ChildRules(c => c.RuleFor(a => a.Address2).MaximumLength(150).WithMessage("Secondary address is too long, max length is 150")
            );
        }
    }
}