using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.Auth
{
    public class AuthenticatedClient
    {
        public AccountType AccountType { get; set; }
        public ClientType ClientType { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public Dictionary<PermissionResource, PermissionType> Permissions { get; set; }
    }
}
