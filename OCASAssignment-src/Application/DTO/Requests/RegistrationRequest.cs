using FluentValidation;
using OCASAPI.Application.DTO.Common;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegistrationRequest
    {
        public string Name {get; set;}
        public AddressDto Address {get; set;}
    }


    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(150).WithMessage("School name must not be at longer than 150 characters long")
                .NotEmpty().WithMessage("Password is required");
            RuleFor(v => v.Address)
                .ChildRules(c => c.RuleFor(a => a.Address1)
                    .Matches(@"^[a-zA-Z0-9\s.,#-_']+$").WithMessage("Primary address contains an invalid character.")
                    .MaximumLength(150).WithMessage("Address is too long, max length is 150"))
                .ChildRules(c => c.RuleFor(a => a.City)
                    .Matches(@"([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$").WithMessage("City contains an invalid character.")
                    .MaximumLength(90).WithMessage("City has max length of 90"))
                .ChildRules(c => c.RuleFor(a => a.Province)
                    .Matches(@"^[A-Za-z\s-_.,']+$").WithMessage("Province contains an invalid character.")
                    .MaximumLength(120).WithMessage("Address is too long, max length is 120"))
                .ChildRules(c => c.RuleFor(a => a.PostalCode)
                    .MaximumLength(120).WithMessage("Address is too long, max length is 120"))
                .ChildRules(c => c.RuleFor(a => a.Address2).MaximumLength(150).WithMessage("Secondary address is too long, max length is 150")
            );
           
        }
    }
}