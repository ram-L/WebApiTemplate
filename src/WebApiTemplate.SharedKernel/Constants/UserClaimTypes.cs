/// <summary>
/// This class defines constant values for user claim types used in identity management.
/// </summary>
namespace WebApiTemplate.SharedKernel.Constants
{
    /// <summary>
    /// Represents user claim types related to identity management.
    /// </summary>
    public static class UserClaimTypes
    {
        private const string Namespace = "http://schemas.example.com/identity/user/claims";

        /// <summary>
        /// Gets the account type claim type.
        /// </summary>
        public const string AccountType = $"{Namespace}/type";

        /// <summary>
        /// Gets the user ID claim type.
        /// </summary>
        public const string UserId = $"{Namespace}/uid";

        /// <summary>
        /// Gets the username claim type.
        /// </summary>
        public const string Username = $"{Namespace}/username";

        /// <summary>
        /// Gets the email claim type.
        /// </summary>
        public const string Email = $"{Namespace}/email";

        /// <summary>
        /// Gets the roles claim type.
        /// </summary>
        public const string Roles = $"{Namespace}/roles";

        /// <summary>
        /// Gets the permissions claim type.
        /// </summary>
        public const string Permissions = $"{Namespace}/permissions";
    }
}
