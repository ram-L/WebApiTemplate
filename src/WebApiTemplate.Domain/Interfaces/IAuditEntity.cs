using System.ComponentModel.DataAnnotations;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Domain.Interfaces
{
    public interface IAuditEntity : IEntity
    {
        /// <summary>
        /// Gets the date when the data was created in Coordinated Universal Time (UTC).
        /// </summary>
        DateTime CreatedDate { get; }

        /// <summary>
        /// Gets the unique identifier of the user or client that created the data.
        /// </summary>
        int? CreatedById { get; }

        /// <summary>
        /// Gets the reference to the user or client that created the data.
        /// </summary>
        Account CreatedBy { get; }

        /// <summary>
        /// Gets the date when the data was last modified in Coordinated Universal Time (UTC).
        /// </summary>
        DateTime? ModifiedDate { get; }

        /// <summary>
        /// Gets the unique identifier of the last user or client that modified the data.
        /// </summary>
        int? ModifiedById { get; }

        /// <summary>
        /// Gets the reference to the last user or client that modified the data.
        /// </summary>
        Account ModifiedBy { get; }

        /// <summary>
        /// Gets the unique identifier of the user who owns the data.
        /// </summary>
        int? OwnerId { get; }

        /// <summary>
        /// Gets the reference to the user who owns the data.
        /// </summary>
        Account Owner { get; }

        /// <summary>
        /// Gets a byte array representing the timestamp for concurrency control.
        /// </summary       
        byte[] RowVersion { get; }

        /// <summary>
        /// Gets a flag indicating whether the data has been soft deleted. The default is false.
        /// </summary>
        bool IsDeleted { get; }
    }
}
