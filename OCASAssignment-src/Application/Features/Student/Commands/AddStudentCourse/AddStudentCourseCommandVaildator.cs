using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddStudentCourseCommandValidator : AbstractValidator<AddStudentCourseCommand>
    {
        public AddStudentCourseCommandValidator()
        {
            RuleFor(c => c.CourseId).NotEmpty().WithMessage("Course id must not be empty");
            RuleFor(c => c.StudentId).NotEmpty().WithMessage("Student id must not be empty");
        }
    }
}