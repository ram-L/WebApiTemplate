using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Interfaces.Api
{
    /// <summary>
    /// Represents the interface for the current user context in the application.
    /// </summary>
    public interface ICurrentUserContext
    {
        /// <summary>
        /// Gets the account type of the current user.
        /// </summary>
        AccountType AccountType { get; }

        /// <summary>
        /// Gets the ID of the current user's account.
        /// </summary>
        int AccountId { get; }

        /// <summary>
        /// Gets a dictionary of permissions associated with the current user or client.
        /// The dictionary maps resource types to permission types.
        /// </summary>
        IDictionary<PermissionResource, PermissionType> Permissions { get; }
    }
}
