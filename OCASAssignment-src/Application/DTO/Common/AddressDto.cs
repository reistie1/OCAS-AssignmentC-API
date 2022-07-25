namespace OCASAPI.Application.DTO.Common
{
    public class AddressDto
    {
        public Guid Id {get; set;}
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string Province {get; set;}
        public string PostalCode {get; set;}
        public string City {get; set;}
    }
}