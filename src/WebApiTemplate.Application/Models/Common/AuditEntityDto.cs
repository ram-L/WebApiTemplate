using System.Text.Json.Serialization;
using WebApiTemplate.SharedKernel.JsonConverters;

namespace WebApiTemplate.Application.Models.Common
{
    public interface IAuditEntityDto
    {
        DateTime? CreatedDate { get; set; }
        int? CreatedById { get; set; }
        DateTime? ModifiedDate { get; set; }
        int? ModifiedById { get; set; }
        int? OwnerId { get; set; }
    }

    public interface IAuditDetailDto
    {
        BaseUserDto CreatedBy { get; set; }
        BaseUserDto ModifiedBy { get; set; }
        BaseUserDto Owner { get; set; }
    }

    public class AuditEntityDto : BaseEntityDto, IAuditEntityDto
    {
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedById { get; set; }


        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public int? OwnerId { get; set; }
    }
}
