using System.Data;
using WebApiTemplate.Domain.Bases;

namespace WebApiTemplate.Domain.Entities
{
    public partial class Role : AuditEntity
    {
        /// <summary>
        /// Gets or sets the unique name or code representing the role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets a description or additional information about the role.
        /// </summary>
        public string Description { get; private set; }


        #region Collections

        public ICollection<AccountRole> Accounts { get; protected set; }

        /// <summary>
        /// Gets or sets the collection of claims associated with this role.
        /// A role can have multiple claims that define its permissions and attributes.
        /// </summary>
        public ICollection<RoleClaim> Claims { get; protected set; }

        #endregion

        public Role()
        {
            Accounts = new List<AccountRole>();
            Claims = new List<RoleClaim>();
        }
    }
}
