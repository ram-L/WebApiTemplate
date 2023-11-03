using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class Role
    {
        /// <summary>
        /// Creates a role with the specified name and description.
        /// </summary>
        /// <param name="currentId">The ID of the current user creating the role.</param>
        /// <param name="name">The name of the role.</param>
        /// <param name="description">The description of the role.</param>
        /// <returns>The newly created role with the specified name and description.</returns>
        public Role Create(int currentId, string name, string description)
        {
            Name = name;
            Description = description;
            SetCreateAudit(currentId);

            return this;
        }

        /// <summary>
        /// Adds a claim to the role, granting a specific permission on a resource.
        /// </summary>
        /// <param name="resource">The resource for which the permission is granted.</param>
        /// <param name="permission">The type of permission to grant.</param>
        public void AddClaim(PermissionResource resource, PermissionType permission)
        {
            Claims.Add(new RoleClaim().Create(resource, permission));
        }
    }
}
