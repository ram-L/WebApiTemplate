using System.ComponentModel.DataAnnotations;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Interfaces;

namespace WebApiTemplate.Domain.Bases
{
    public abstract partial class AuditEntity : BaseEntity, IAuditEntity
    {
        public DateTime CreatedDate { get; protected set; }

        public int? CreatedById { get; protected set; }
        public virtual Account CreatedBy { get; protected set; }

        public DateTime? ModifiedDate { get; protected set; }

        public int? ModifiedById { get; protected set; }
        public virtual Account ModifiedBy { get; protected set; }

        public int? OwnerId { get; set; }
        public virtual Account Owner { get; protected set; }

        public byte[] RowVersion { get; protected set; }

        public bool IsDeleted { get; protected set; } = false;
    }
}
