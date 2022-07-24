namespace OCAS.Domain.Common
{
    public class Address : BaseEntity
    {
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string Province {get; set;}
        public string PostalCode {get; set;}
        public string City {get; set;}
    }
}
