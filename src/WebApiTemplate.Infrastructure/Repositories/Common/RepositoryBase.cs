using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using System.Linq.Expressions;
using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Extensions;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Infrastructure.Repositories.Common
{
    /// <summary>
    /// Base class for implementing a generic repository.
    /// </summary>
    public abstract class RepositoryBase : IRepository
    {
        protected readonly IAppDbContextProvider _provider;
        protected readonly ICustomLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="provider">The database context provider.</param>
        /// <param name="logger">The custom logger.</param>
        protected RepositoryBase(IAppDbContextProvider provider, ICustomLogger logger)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public virtual TReturn FuncOnError<TReturn>(ErrorHandlingOption action, Exception ex)
        {
            switch (action)
            {
                default:
                case ErrorHandlingOption.Throw:
                    throw ex;

                case ErrorHandlingOption.None:
                case ErrorHandlingOption.ReturnDefault:
                    _logger.LogMessage("Returned default value with error.", LogEventLevel.Fatal, ex);
                    return default;

                case ErrorHandlingOption.ReturnEmpty:
                    _logger.LogMessage("Returned empty value with error.", LogEventLevel.Fatal, ex);
                    return typeof(TReturn).GetEmptyOrDefaultValue().ConvertTo<TReturn>();
            }
        }

        /// <inheritdoc/>
        public virtual Task<TReturn> FuncOnErrorAsync<TReturn>(ErrorHandlingOption action, Exception ex)
        {
            switch (action)
            {
                default:
                case ErrorHandlingOption.Throw:
                    return Task.FromException<TReturn>(ex);

                case ErrorHandlingOption.None:
                case ErrorHandlingOption.ReturnDefault:
                    _logger.LogMessage("Returned default value with error.", LogEventLevel.Fatal, ex);
                    return Task.FromResult(default(TReturn));

                case ErrorHandlingOption.ReturnEmpty:
                    _logger.LogMessage("Returned empty value with error.", LogEventLevel.Fatal, ex);
                    return Task.FromResult(typeof(TReturn).GetEmptyOrDefaultValue().ConvertTo<TReturn>());
            }
        }

        /// <inheritdoc/>
        public virtual void ActionOnError(ErrorHandlingOption action, Exception ex)
        {
            switch (action)
            {
                case ErrorHandlingOption.Throw:
                    throw ex;

                case ErrorHandlingOption.None:
                    _logger.LogMessage("Error found.", LogEventLevel.Fatal, ex);
                    break;
            }
        }

        /// <inheritdoc/>
        public virtual Task ActionOnErrorAsync(ErrorHandlingOption action, Exception ex)
        {
            switch (action)
            {
                case ErrorHandlingOption.Throw:
                    return Task.FromException(ex);

                case ErrorHandlingOption.None:
                    _logger.LogMessage("Error found.", LogEventLevel.Fatal, ex);
                    break;
            }

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Base class for implementing a generic repository for a specific entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity> where TEntity : class
    {
        protected const int minPageNumber = 1;
        protected const int maxPageSize = 250;

        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="provider">The database context provider.</param>
        /// <param name="logger">The custom logger.</param>
        protected RepositoryBase(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger)
        {
            _dbSet = _provider.Context.Set<TEntity>();
        }

        public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                return await _dbSet.Where(predicate).AnyAsync();
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<bool>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> FindByAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = false,
            EntityLoader<TEntity> entityLoader = null,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                // Building the query with the necessary includes.
                IQueryable<TEntity> query = _dbSet;

                //loads include if any
                if (entityLoader != null)
                    query = await entityLoader.LoadIncludesAsync(query);

                // Applying NoTracking if requested.
                if (noTracking)
                    query = query.AsNoTracking();

                // Executing the query.
                var result = await query.Where(predicate).FirstOrDefaultAsync();

                if (entityLoader != null)
                    await entityLoader.LoadExplicitsAsync(_provider.Context, result);

                return result;
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<TEntity>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                return await _dbSet.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<int>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IList<TEntity>> ListByAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = false,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.Where(predicate);

                if (noTracking)
                    query = query.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<IList<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> QueryBy(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = false,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.Where(predicate);

                if (noTracking)
                    query = query.AsNoTracking();

                return query;
            }
            catch (Exception ex)
            {
                return FuncOnError<IQueryable<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetBareQuery(
            bool noTracking = false,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                return noTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
            }
            catch (Exception ex)
            {
                return FuncOnError<IQueryable<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<PagedList<TEntity>> PagedListAsync(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber,
            int pageSize,
            bool noTracking = false,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                pageNumber = Math.Max(pageNumber, minPageNumber);
                pageSize = Math.Min(pageSize, maxPageSize);

                IQueryable<TEntity> query = _dbSet.Where(predicate);

                if (noTracking)
                    query = query.AsNoTracking();

                return await PagedList<TEntity>.ToPagedListAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<PagedList<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<OperationResult> InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbSet.Add(entity);
                await _provider.Context.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogMessage("An error occurred while creating the record.", LogEventLevel.Fatal, ex);
                return OperationResult.Failure("An error occurred while creating the record. Please try again or contact the administrator.");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<OperationResult> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var entry = _provider.Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }

                await _provider.Context.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogMessage("An error occurred while updating the record.", LogEventLevel.Fatal, ex);
                return OperationResult.Failure("An error occurred while updating the record. Please try again or contact the administrator.");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<OperationResult> DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbSet.Remove(entity);
                await _provider.Context.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogMessage("An error occurred while deleting the record.", LogEventLevel.Fatal, ex);
                return OperationResult.Failure("An error occurred while deleting the record. Please try again or contact the administrator.");
            }
        }

        /// <inheritdoc/>
        public virtual async Task LoadExplicitsAsync(TEntity entity, params Expression<Func<TEntity, object>>[] explicits)
        {
            var entityLoader = new EntityLoader<TEntity>();
            entityLoader.AddExplicits(explicits.ToArray());
            await entityLoader.LoadExplicitsAsync(_provider.Context, entity);
        }

        /// <inheritdoc/>
        public virtual async Task LoadExplicitsAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] explicits)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    await LoadExplicitsAsync(entity, explicits);
                }
            }
        }
    }
}
