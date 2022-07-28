using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class UpdateSchoolTeacherCommandValidator : AbstractValidator<UpdateSchoolTeacherCommand>
    {
        public UpdateSchoolTeacherCommandValidator()
        {
            RuleFor(c => c.Teacher.FirstName)
                .NotEmpty().WithMessage("First name must not be empty")
                .MaximumLength(100).WithMessage("First name must not be greater than length of 100 characters")
                .Matches(@"^[A-Za-z0-9\s-_*]+$").WithMessage("First name contains an invalid character.");
            RuleFor(c => c.Teacher.LastName)
                .NotEmpty().WithMessage("Last code must not be empty")
                .MaximumLength(100).WithMessage("Last code must not have length greater than 100 characters")
                .Matches(@"^[A-Za-z0-9\s-_*.]+$").WithMessage("Last code contains an invalid character.");
            RuleFor(c => c.Teacher.SchoolId)
                .NotEmpty().WithMessage("Student school id must not be empty");
            RuleFor(c => c.Teacher.SubjectClassifier)
                .NotEmpty().WithMessage("Subject classifier must not be empty")
                .MaximumLength(100).WithMessage("Subject classifier must not have length greater than 100 characters")
                .Matches(@"^[A-Za-z\s*.,]+$").WithMessage("Subject classifier contains an invalid character.");
        }
    }
}