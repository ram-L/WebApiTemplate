using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Interfaces.Infrastructure
{
    public interface IClientRepository : IAuditEntityRepository<Account> { }
}
