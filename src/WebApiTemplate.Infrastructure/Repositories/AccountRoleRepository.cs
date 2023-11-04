using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Common;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories
{
    public class AccountRoleRepository : RepositoryBase<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger) { }
    }
}
