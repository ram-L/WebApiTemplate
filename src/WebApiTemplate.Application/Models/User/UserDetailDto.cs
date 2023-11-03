using WebApiTemplate.Application.Models.Common;

namespace WebApiTemplate.Application.Models.User
{
    public class UserDetailDto : UserSummaryDto, IAuditDetailDto
    {
        public BaseUserDto CreatedBy { get; set; }
        public BaseUserDto ModifiedBy { get; set; }
        public BaseUserDto Owner { get; set; }
    }
}
