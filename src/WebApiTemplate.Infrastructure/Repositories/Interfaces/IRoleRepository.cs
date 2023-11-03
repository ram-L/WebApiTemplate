using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Repositories.Interfaces;

namespace NetCoreApiTemplate.Infrastructure.Repositories.Interfaces
{
    public interface IRoleRepository : IAuditEntityRepository<Role> { }
}
