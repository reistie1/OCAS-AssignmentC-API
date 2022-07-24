using OCASAPI.Application.DTO.Dto;

namespace OCASAPI.Application.DTO.Requests
{
    public class RegistrationRequest
    {
        public string Name {get; set;}
        public AddressDto Address {get; set;}
    }
}