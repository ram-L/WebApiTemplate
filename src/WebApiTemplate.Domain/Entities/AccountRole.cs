using WebApiTemplate.Domain.Bases;

namespace WebApiTemplate.Domain.Entities
{
    /// <summary>
    /// Represents a relationship between an account and a role entity.
    /// </summary>
    public partial class AccountRole : BaseEntity
    {
        /// <summary>
        /// Gets or sets the account ID associated with this account-role relationship.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Gets or sets the associated account for this relationship.
        /// </summary>
        public virtual Account Account { get; protected set; }

        /// <summary>
        /// Gets or sets the role ID associated with this account-role relationship.
        /// </summary>
        public int RoleId { get; protected set; }

        /// <summary>
        /// Gets or sets the associated role for this relationship.
        /// </summary>
        public virtual Role Role { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRole"/> class.
        /// </summary>
        public AccountRole()
        {
            // Default constructor
        }
    }
}
