/// <summary>
/// This class defines constant values for client claim types used in identity management.
/// </summary>
namespace WebApiTemplate.SharedKernel.Constants
{
    /// <summary>
    /// Represents client claim types related to identity management.
    /// </summary>
    public static class ClientClaimTypes
    {
        private const string Namespace = "http://schemas.example.com/identity/client/claims";

        /// <summary>
        /// Gets the account type claim type for clients.
        /// </summary>
        public const string AccountType = $"{Namespace}/type";

        /// <summary>
        /// Gets the client type claim type.
        /// </summary>
        public const string ClientType = $"{Namespace}/clienttype";

        /// <summary>
        /// Gets the client ID claim type.
        /// </summary>
        public const string ClientId = $"{Namespace}/cid";

        /// <summary>
        /// Gets the client name claim type.
        /// </summary>
        public const string ClientName = $"{Namespace}/name";

        /// <summary>
        /// Gets the roles claim type for clients.
        /// </summary>
        public const string Roles = $"{Namespace}/roles";

        /// <summary>
        /// Gets the permissions claim type for clients.
        /// </summary>
        public const string Permissions = $"{Namespace}/permissions";
    }
}
