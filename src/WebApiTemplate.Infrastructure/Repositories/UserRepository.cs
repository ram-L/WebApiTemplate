using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories
{
    public class UserRepository : AuditEntityRepositoryBase<Account>, IUserRepository
    {
        public UserRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
