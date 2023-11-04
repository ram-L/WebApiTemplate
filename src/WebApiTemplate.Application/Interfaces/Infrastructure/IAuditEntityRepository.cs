namespace WebApiTemplate.Application.Interfaces.Infrastructure
{
    public interface IAuditEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
