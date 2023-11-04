using Newtonsoft.Json;
using WebApiTemplate.Application.Interfaces.Api;
using WebApiTemplate.SharedKernel.Constants;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Extensions;

namespace WebApiTemplate.Api.Authorization
{
    public class HttpCurrentUserContext : ICurrentUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCurrentUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AccountType AccountType => (AccountType)_httpContextAccessor.HttpContext?.User?.FindFirst(UserClaimTypes.AccountType)?.Value.ToAccountType(AccountType.User);

        public int AccountId => (int)_httpContextAccessor.HttpContext?.User?.FindFirst(UserClaimTypes.UserId)?.Value?.ToInt(0);

        public IDictionary<PermissionResource, PermissionType> Permissions
        {
            get
            {
                var permissonClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(UserClaimTypes.Permissions)?.Value;
                if(permissonClaim == null)
                    return new Dictionary<PermissionResource, PermissionType>();

                return JsonConvert.DeserializeObject<Dictionary<PermissionResource, PermissionType>>(permissonClaim);
            }
        }
    }
}
