using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class UpdateSchoolTeacherCommandValidator : AbstractValidator<UpdateSchoolTeacherCommand>
    {
        public UpdateSchoolTeacherCommandValidator()
        {
           
        }
    }
}