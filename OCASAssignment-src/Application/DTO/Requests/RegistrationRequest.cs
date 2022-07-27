using System.ComponentModel.DataAnnotations;
using FluentValidation;
using OCASAPI.Application.DTO.Common;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegistrationRequest
    {
        public string Name {get; set;}
        public AddressDto Address {get; set;}
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string DefaultPassword { get; set; }
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
             RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.FirstName)
                .MaximumLength(100).WithMessage("First name is too long")
                .NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Last name is too long")
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("User must have a role");
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(16).WithMessage("Phone number is too long");
            RuleFor(x => x.DefaultPassword)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .NotEmpty().WithMessage("Password is required");
           
        }
    }
}