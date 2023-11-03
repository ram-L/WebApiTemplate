using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class ExternalUserProfile
    {
        public ExternalUserProfile Create(AuthProvider authProvider, string email, string providerUserId, string tenantId)
        {
            AuthProvider = authProvider;
            Email = email;
            ProviderUserId = providerUserId;
            TenantId = tenantId;

            return this;
        }

        public void Update()
        {
        }
    }
}
