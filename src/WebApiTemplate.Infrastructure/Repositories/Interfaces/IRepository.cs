using System.Linq.Expressions;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Provides a generic repository interface for database operations.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Executes a function and handles errors based on the specified action.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the function.</typeparam>
        /// <param name="action">The error handling action to take.</param>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>The result of the function or a default value based on the error handling action.</returns>
        TReturn FuncOnError<TReturn>(ErrorHandlingOption action, Exception ex);

        /// <summary>
        /// Executes an asynchronous function and handles errors based on the specified action.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the asynchronous function.</typeparam>
        /// <param name="action">The error handling action to take.</param>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>The result of the asynchronous function or a default value based on the error handling action.</returns>
        Task<TReturn> FuncOnErrorAsync<TReturn>(ErrorHandlingOption action, Exception ex);

        /// <summary>
        /// Performs an action and handles errors based on the specified action.
        /// </summary>
        /// <param name="action">The error handling action to take.</param>
        /// <param name="ex">The exception to handle.</param>
        void ActionOnError(ErrorHandlingOption action, Exception ex);

        /// <summary>
        /// Performs an asynchronous action and handles errors based on the specified action.
        /// </summary>
        /// <param name="action">The error handling action to take.</param>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ActionOnErrorAsync(ErrorHandlingOption action, Exception ex);
    }

    /// <summary>
    /// Provides a generic repository interface for database operations on a specific entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        /// <summary>
        /// Checks if any records exist that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter records.</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>A task that represents whether any matching records exist.</returns>
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Asynchronously retrieves a single entity that matches the specified predicate from the DbSet.
        /// </summary>
        /// <param name="predicate">A predicate expression to filter entities.</param>
        /// <param name="noTracking">Indicates whether to query the entity without tracking changes.</param>
        /// <param name="entityLoader">An optional entity loader to handle includes and explicit loading.</param>
        /// <param name="action">An error handling option to control how exceptions are handled.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns the matching entity, or null if not found.
        /// If an exception occurs and the specified error handling option is set to 'Throw', the exception is propagated.
        /// </returns>
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, EntityLoader<TEntity> entityLoader = null, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Counts the number of records that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter records.</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>A task that represents the count of matching records.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Searches for and returns a list of records that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter records.</param>
        /// <param name="noTracking">Indicates whether to use query tracking (default is false).</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>A task that represents a list of matching entities.</returns>
        Task<IList<TEntity>> ListByAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Returns a queryable collection of entities that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter records.</param>
        /// <param name="noTracking">Indicates whether to use query tracking (default is false).</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>An IQueryable collection of matching entities.</returns>
        IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Returns a queryable collection of all entities.
        /// </summary>
        /// <param name="noTracking">Indicates whether to use query tracking (default is false).</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>An IQueryable collection of all entities.</returns>
        IQueryable<TEntity> GetBareQuery(bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Gets a paged list of entities that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter records.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="noTracking">Indicates whether to use query tracking (default is false).</param>
        /// <param name="action">The error handling action to take (default is ErrorHandlingOption.Throw).</param>
        /// <returns>A task that represents a paged list of matching entities.</returns>
        Task<PagedList<TEntity>> PagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw);

        /// <summary>
        /// Inserts a single entity into the repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <remarks>
        /// This method adds the provided entity instance to the repository’s underlying data set and attempts to save the changes to the database.
        /// If the operation fails, an exception will be thrown.
        /// Ensure that calling code is prepared to handle these exceptions as needed.
        /// </remarks>
        /// <returns>A task representing the asynchronous insert operation. The task result is an OperationResult that contains the outcome of the operation.</returns>
        Task<OperationResult> InsertAsync(TEntity entity);

        /// <summary>
        /// Updates a single entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <remarks>
        /// This method updates the properties of an existing entity in the repository’s underlying data set and attempts to save the changes to the database.
        /// If the entity is not already tracked by the context, it will be attached before saving.
        /// If the operation fails, an exception will be thrown.
        /// Ensure that calling code is prepared to handle these exceptions as needed.
        /// </remarks>
        /// <returns>A task representing the asynchronous update operation. The task result is an OperationResult that contains the outcome of the operation.</returns>
        Task<OperationResult> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes a single entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <remarks>
        /// This method removes the provided entity instance from the repository’s underlying data set and attempts to save the changes to the database.
        /// If the operation fails, an exception will be thrown.
        /// Ensure that calling code is prepared to handle these exceptions as needed.
        /// </remarks>
        /// <returns>A task representing the asynchronous delete operation. The task result is an OperationResult that contains the outcome of the operation.</returns>
        Task<OperationResult> DeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously loads explicit navigation properties of the given entity using a specified list of expressions.
        /// </summary>
        /// <param name="entity">The entity whose explicit navigation properties should be loaded.</param>
        /// <param name="explicits">A list of expressions representing the explicit navigation properties to load.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method asynchronously loads the explicit navigation properties of the specified entity
        /// using the provided list of expressions. It uses an <see cref="EntityLoader{TEntity}"/> instance
        /// to perform the loading operation.
        /// </remarks>
        Task LoadExplicitsAsync(TEntity entity, params Expression<Func<TEntity, object>>[] explicits);

        /// <summary>
        /// Asynchronously loads the explicit navigation properties for a collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities for which to load the explicit navigation properties.</param>
        /// <param name="explicits">An array of expressions representing the navigation properties to be explicitly loaded.</param>
        /// <remarks>
        /// This method iterates over the provided collection of entities and loads the specified navigation properties for each entity.
        /// </remarks>
        Task LoadExplicitsAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] explicits);
    }
}
