using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddCourseCommandValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseCommandValidator()
        {
            RuleFor(c => c.Course.CourseName)
                .NotEmpty().WithMessage("Course name must not be empty")
                .MaximumLength(100).WithMessage("Course name must not be greater than length of 100 characters")
                .Matches(@"^[A-Za-z0-9\s-_*]+$").WithMessage("Course name contains an invalid character.");
            RuleFor(c => c.Course.CourseCode)
                .NotEmpty().WithMessage("Course code must not be empty")
                .MaximumLength(10).WithMessage("Course code must not have length greater than 10 characters")
                .Matches(@"^[A-Za-z0-9\s-_*.]+$").WithMessage("Course code contains an invalid character.");
            RuleFor(c => c.Course.SchoolId)
                .NotEmpty().WithMessage("Course school id must not be empty");
            RuleFor(c => c.Course.Description)
                .NotEmpty().WithMessage("Course description must not be empty")
                .MaximumLength(255).WithMessage("Course description must not have length greater than 255 characters")
                .Matches(@"^[A-Za-z0-9\s-_*.,']+$").WithMessage("Course description contains an invalid character.");
        }
    }
}