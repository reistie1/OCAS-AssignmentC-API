using System;
namespace OCAS.Domain.Common
{
    public class Tokens : BaseEntity
    {
        public Guid UserId {get; set;}
        public string Token {get; set;}
        public DateTime Expiration {get; set;}
        // public bool Expired {get; set;} = ;
        // public bool IsValid {get; set;} = ;
    }
}
