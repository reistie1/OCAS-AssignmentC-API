using FluentValidation;
using OCASAPI.Application.Features;

namespace OCASAPI.Application.Validators
{
    public class GetJoinedActivityListCommandValidator : AbstractValidator<GetJoinedActivityListCommand>
    {
        public GetJoinedActivityListCommandValidator()
        {
            RuleFor(c => c.ActivityId)
                .NotEmpty().WithMessage("Activity id must not be empty.");
        }   
    }
}