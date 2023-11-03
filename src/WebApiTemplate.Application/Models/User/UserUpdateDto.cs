using System.Text.Json.Serialization;
using WebApiTemplate.Application.Models.Common;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Models.User
{
    public class UserUpdateDto : BaseEntityDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus Status { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
