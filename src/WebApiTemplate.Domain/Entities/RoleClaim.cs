using System.ComponentModel.DataAnnotations;
using WebApiTemplate.Domain.Bases;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class RoleClaim : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role associated with this role claim.
        /// </summary>
        public int RoleId { get; protected set; }

        /// <summary>
        /// Gets or sets the reference to the role associated with this role claim.
        /// </summary>
        public virtual Role Role { get; protected set; }

        /// <summary>
        /// Gets or sets the resource or entity to which the permission applies.
        /// </summary>
        public PermissionResource Resource { get; private set; }

        /// <summary>
        /// Gets or sets the type of permission (CRUD - Create, Read, Update, Delete).
        /// </summary>
        public PermissionType Permission { get; private set; }
    }
}
