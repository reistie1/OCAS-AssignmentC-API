using OCASAPI.Application.DTO.Common;

namespace OCASAPI.Application.DTO.Requests
{
    public class SchoolInfoRequest
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public AddressDto Address {get; set;}
    }
}