using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.Auth
{
    public class ExternalLoginDto
    {
        public AuthProvider Provider { get; set; }
        public string Code { get; set; }
    }
}
