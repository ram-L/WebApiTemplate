using WebApiTemplate.Domain.Bases;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    /// <summary>
    /// Represents an external user profile entity.
    /// </summary>
    public partial class ExternalUserProfile : BaseEntity
    {
        /// <summary>
        /// Gets or sets the account ID associated with the external user profile.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Gets or sets the associated account for the external user profile.
        /// </summary>
        public virtual Account Account { get; protected set; }

        /// <summary>
        /// Gets or sets the authentication provider used for this external user profile.
        /// </summary>
        public AuthProvider AuthProvider { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the external user profile.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the provider-specific user ID for this external user profile.
        /// </summary>
        public string ProviderUserId { get; set; }

        /// <summary>
        /// Gets or sets the Tenant ID, specifically for Azure Active Directory (Azure AD).
        /// </summary>
        public string TenantId { get; set; }
    }
}
