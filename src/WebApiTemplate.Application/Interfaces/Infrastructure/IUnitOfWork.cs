using EFCore.BulkExtensions;

namespace WebApiTemplate.Application.Interfaces.Infrastructure
{
    /// <summary>
    /// Represents a unit of work that can be used to group one or more operations into a single transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a new transaction.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commits the current transaction.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task CommitAsync();

        /// <summary>
        /// Rolls back the current transaction.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task RollbackAsync();

        /// <summary>
        /// Asynchronously inserts a new entity into the context's DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Marks an entity as modified in the context's DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Asynchronously removes an entity from the context's DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
    }

}
