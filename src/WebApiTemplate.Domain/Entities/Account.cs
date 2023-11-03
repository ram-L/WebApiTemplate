using WebApiTemplate.Domain.Bases;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    /// <summary>
    /// Represents an account entity.
    /// </summary>
    public partial class Account : AuditEntity
    {
        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        public AccountType AccountType { get; protected set; }

        /// <summary>
        /// Gets or sets the status of the account.
        /// </summary>
        public AccountStatus Status { get; protected set; }

        #region Collections

        /// <summary>
        /// Gets or sets the roles associated with the account.
        /// </summary>
        public ICollection<AccountRole> Roles { get; protected set; }

        #endregion

        /// <summary>
        /// Gets or sets the user profile associated with the account.
        /// </summary>
        public virtual UserProfile UserProfile { get; protected set; }

        /// <summary>
        /// Gets or sets the client profile associated with the account.
        /// </summary>
        public virtual ClientProfile ClientProfile { get; protected set; }

        /// <summary>
        /// Gets or sets the external user profile associated with the account.
        /// </summary>
        public virtual ExternalUserProfile ExternalUserProfile { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        public Account()
        {
            Roles = new List<AccountRole>();
        }
    }
}
