using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Interfaces.Infrastructure
{
    public interface IUserRepository : IAuditEntityRepository<Account> { }
}
