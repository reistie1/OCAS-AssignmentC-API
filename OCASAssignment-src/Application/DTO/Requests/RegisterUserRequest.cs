using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegisterUserRequest
    {
        
    }

    public class RegistrationViewModelValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegistrationViewModelValidator()
        {
           
        }
    }
}


