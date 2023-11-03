using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApiTemplate.Application.Models.Common;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.Client
{
    public class ClientSummaryDto : AuditEntityDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountType AccessType { get; set; }

        public string ClientId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStatus Status { get; set; }
    }
}
