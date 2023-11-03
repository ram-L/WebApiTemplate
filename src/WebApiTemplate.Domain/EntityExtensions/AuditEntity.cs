namespace WebApiTemplate.Domain.Bases
{
    /// <summary>
    /// Represents an abstract base class for entities that require auditing.
    /// </summary>
    public abstract partial class AuditEntity
    {
        /// <summary>
        /// Sets the audit information for entity creation.
        /// </summary>
        /// <param name="accountId">The ID of the user creating the entity (optional).</param>
        /// <param name="dateTime">The date and time of creation (optional).</param>
        public void SetCreateAudit(int? accountId = null, DateTime? dateTime = null)
        {
            CreatedDate = dateTime ?? DateTime.UtcNow;
            CreatedById = accountId;
            OwnerId = accountId;
            UpdateState(SharedKernel.Enums.TrackingState.Added);
        }

        /// <summary>
        /// Sets the audit information for entity modification.
        /// </summary>
        /// <param name="accountId">The ID of the user making the modification.</param>
        /// <param name="dateTime">The date and time of modification (optional).</param>
        public void SetUpdateAudit(int accountId, DateTime? dateTime = null)
        {
            ModifiedDate = dateTime ?? DateTime.UtcNow;
            ModifiedById = accountId;
            UpdateState(SharedKernel.Enums.TrackingState.Modified);
        }

        /// <summary>
        /// Sets the audit information for entity deletion.
        /// </summary>
        /// <param name="accountId">The ID of the user initiating the deletion.</param>
        /// <param name="dateTime">The date and time of deletion (optional).</param>
        /// <param name="hardDelete">A flag indicating whether to perform a hard delete (optional).</param>
        public void SetDeleteAudit(int accountId, DateTime? dateTime = null, bool hardDelete = false)
        {
            if (hardDelete)
            {
                UpdateState(SharedKernel.Enums.TrackingState.HardDeleted);
            }
            else
            {
                IsDeleted = true;
                SetUpdateAudit(accountId, dateTime);
                UpdateState(SharedKernel.Enums.TrackingState.SoftDeleted);
            }
        }
    }
}
