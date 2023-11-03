using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.RoleClaim
{
    public class RoleClaimDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RoleName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PermissionResource Resource { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PermissionType Permission { get; set; }
    }
}
