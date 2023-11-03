using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.Common
{
    public class BaseUserDto : BaseEntityDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountType AccountType { get; set; }

        public string Username { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStatus Status { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Fullname { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
