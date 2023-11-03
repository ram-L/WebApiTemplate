using WebApiTemplate.Application.Models.Common;

namespace WebApiTemplate.Application.Models.Client
{
    public class ClientDetailDto : ClientSummaryDto, IAuditDetailDto
    {
        public BaseUserDto CreatedBy { get; set; }
        public BaseUserDto ModifiedBy { get; set; }
        public BaseUserDto Owner { get; set; }
    }
}
