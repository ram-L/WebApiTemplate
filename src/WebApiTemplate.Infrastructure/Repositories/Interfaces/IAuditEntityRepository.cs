namespace WebApiTemplate.Infrastructure.Repositories.Interfaces
{
    public interface IAuditEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
