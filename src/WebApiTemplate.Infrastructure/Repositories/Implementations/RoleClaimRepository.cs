using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.Infrastructure.Repositories.Interfaces;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories.Implementations
{
    public class RoleClaimRepository : RepositoryBase<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
