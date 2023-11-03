using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.Auth
{
    public class AuthenticatedUser
    {
        public AccountType AccountType { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Dictionary<PermissionResource, PermissionType> Permissions { get; set; }
    }
}
