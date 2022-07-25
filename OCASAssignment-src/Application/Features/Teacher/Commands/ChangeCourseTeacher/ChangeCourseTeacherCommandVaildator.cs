using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class ChangeCourseTeacherCommandValidator : AbstractValidator<ChangeCourseTeacherCommand>
    {
        public ChangeCourseTeacherCommandValidator()
        {
            RuleFor(c => c.TeacherChange.CourseId).NotEmpty().WithMessage("Course id must not be empty");
            RuleFor(c => c.TeacherChange.OldTeacherId).NotEmpty().WithMessage("Old Teacher id must not be empty");
            RuleFor(c => c.TeacherChange.NewTeacherId).NotEmpty().WithMessage("New Teacher id must not be empty");
        }
    }
}