using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories
{
    public class RoleRepository : AuditEntityRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
