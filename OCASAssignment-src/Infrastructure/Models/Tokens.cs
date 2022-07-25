namespace OCASAPI.Infrastructure.Models
{
    public class Tokens
    {
        public Guid UserId {get; set;}
        public string Token {get; set;}
        public DateTime Expiration {get; set;}
        
    }
}
