namespace OCASAPI.Application.Requests
{
    public class ActivityPersonResponse
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public Guid ActivityId {get; set;}
        public string Comments {get; set;}
        public DateTime SignedUpDate {get; set;}
        public char Gender {get; set;}
    }
}