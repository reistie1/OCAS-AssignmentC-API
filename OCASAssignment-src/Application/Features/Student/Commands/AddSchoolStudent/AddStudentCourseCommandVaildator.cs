using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddSchoolStudentCommandValidator : AbstractValidator<AddSchoolStudentCommand>
    {
        public AddSchoolStudentCommandValidator()
        {
             RuleFor(c => c.Student.FirstName)
                .NotEmpty().WithMessage("First name must not be empty")
                .MaximumLength(100).WithMessage("First name must not be greater than length of 100 characters")
                .Matches(@"^[A-Za-z0-9\s-_*]+$").WithMessage("First name contains an invalid character.");
            RuleFor(c => c.Student.LastName)
                .NotEmpty().WithMessage("Last code must not be empty")
                .MaximumLength(10).WithMessage("Last code must not have length greater than 10 characters")
                .Matches(@"^[A-Za-z0-9\s-_*.]+$").WithMessage("Last code contains an invalid character.");
            RuleFor(c => c.Student.SchoolId)
                .NotEmpty().WithMessage("Student school id must not be empty");
            RuleFor(c => c.Student.Age)
                .InclusiveBetween(0, 100).WithMessage("Age range invalid");
        }
    }
}