using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class RoleClaim
    {
        /// <summary>
        /// Creates a role claim with the specified permission on a resource.
        /// </summary>
        /// <param name="resource">The resource for which the permission is granted.</param>
        /// <param name="permission">The type of permission to grant.</param>
        /// <returns>The newly created role claim with the specified permission and resource.</returns>
        public RoleClaim Create(PermissionResource resource, PermissionType permission)
        {
            Resource = resource;
            Permission = permission;
            return this;
        }
    }
}
