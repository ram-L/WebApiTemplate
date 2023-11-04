using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories
{
    public class ClientRepository : AuditEntityRepositoryBase<Account>, IClientRepository
    {
        public ClientRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
