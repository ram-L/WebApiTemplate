using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IAuditEntityRepository<Account> { }
}
