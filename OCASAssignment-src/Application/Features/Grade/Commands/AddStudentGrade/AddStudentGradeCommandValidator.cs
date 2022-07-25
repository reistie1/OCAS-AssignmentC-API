using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class AddStudentGradeCommandValidator : AbstractValidator<AddStudentGradeCommand>
    {
        public AddStudentGradeCommandValidator()
        {
            RuleFor(c => c.Grade.NumericGrade)                
                .NotEmpty().WithMessage("Numerics grade value must not be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Numeric grade must be greater of equal to 0")
                .InclusiveBetween(0, 100).WithMessage("Numeric grade is not in range");
            RuleFor(c => c.Grade.StudentId)                
                .NotEmpty().WithMessage("Student Id must not be empty");
            RuleFor(c => c.Grade.CourseId)                
                .NotEmpty().WithMessage("Course Id must not be empty");
            RuleFor(c => c.Grade.AlphabeticGrade)                
                .NotEmpty().WithMessage("Course Id must not be empty")
                .InclusiveBetween('A', 'F').WithMessage("Alphabetic grade is not in range");
        }
    }
}