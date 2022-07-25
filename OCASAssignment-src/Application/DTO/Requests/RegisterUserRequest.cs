using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegisterUserRequest
    {
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

    public class RegistrationViewModelValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegistrationViewModelValidator()
        {
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


