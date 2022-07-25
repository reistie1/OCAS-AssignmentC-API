using OCASAPI.Application.DTO.Common;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegistrationRequest
    {
        public string Name {get; set;}
        public AddressDto Address {get; set;}
    }
}