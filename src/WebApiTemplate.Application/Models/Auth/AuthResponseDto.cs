using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Application.Models.Auth
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Dictionary<PermissionResource, string> Permissions { get; set; }

        public AuthResponseDto()
        {
            Success = true;
            Permissions = new Dictionary<PermissionResource, string>();
        }

        public AuthResponseDto(bool _success = true) : this()
        {
            Success = _success;
        }
    }
}
