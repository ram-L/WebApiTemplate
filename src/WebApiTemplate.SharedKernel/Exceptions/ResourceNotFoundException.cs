using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a requested resource is not found.
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with the specified resource name and ID.
        /// </summary>
        /// <param name="resourceName">The name of the resource.</param>
        /// <param name="resourceId">The ID of the resource.</param>
        public ResourceNotFoundException(string resourceName, int resourceId)
            : base($"{resourceName} with ID {resourceId} not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with the specified permission resource and ID.
        /// </summary>
        /// <param name="resourceName">The permission resource.</param>
        /// <param name="resourceId">The ID of the resource.</param>
        public ResourceNotFoundException(PermissionResource resourceName, int resourceId)
            : base($"{resourceName} with ID {resourceId} not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with the specified permission resource and filter.
        /// </summary>
        /// <param name="resourceName">The permission resource.</param>
        /// <param name="filter">The filter condition (optional).</param>
        public ResourceNotFoundException(PermissionResource resourceName, string filter = "")
            : base(string.Format("{0} not found {1}", resourceName, string.IsNullOrEmpty(filter) ? "" : " with filter " + filter))
        {
        }
    }

}
