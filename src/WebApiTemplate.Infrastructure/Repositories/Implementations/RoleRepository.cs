using NetCoreApiTemplate.Infrastructure.Repositories.Interfaces;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.Infrastructure.Repositories.Interfaces;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories.Implementations
{
    public class RoleRepository : AuditEntityRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
