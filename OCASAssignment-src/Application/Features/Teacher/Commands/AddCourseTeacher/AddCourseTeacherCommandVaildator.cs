using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddCourseTeacherCommandValidator : AbstractValidator<AddCourseTeacherCommand>
    {
        public AddCourseTeacherCommandValidator()
        {
            RuleFor(c => c.CourseId).NotEmpty().WithMessage("Course id must not be empty");
            RuleFor(c => c.TeacherId).NotEmpty().WithMessage("Teacher id must not be empty");
        }
    }
}