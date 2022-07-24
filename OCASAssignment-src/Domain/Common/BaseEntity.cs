using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class BaseEntity : AuditableBaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Timestamp]  
        public byte[] RowVersion { get; set; } 
    }
}