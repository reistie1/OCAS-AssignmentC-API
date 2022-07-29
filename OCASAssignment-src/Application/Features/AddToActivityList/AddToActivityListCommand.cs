using MediatR;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddToActivityListCommand : IRequest<Response<bool>>
    {
        public AddToActivityListCommand(ActivityPersonRequest person)
        {
            Person = person;
        }

        public ActivityPersonRequest Person { get; }
    }
}