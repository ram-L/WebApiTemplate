using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiTemplate.Domain.Interfaces;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Infrastructure.Repositories.Common
{
    /// <summary>
    /// Base repository class for entities that implement IAuditEntity.
    /// Provides common functionality for auditing entity changes.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that implements IAuditEntity.</typeparam>
    public abstract class AuditEntityRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class, IAuditEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntityRepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="provider">The database context provider.</param>
        /// <param name="logger">The custom logger.</param>
        protected AuditEntityRepositoryBase(IAppDbContextProvider provider, ICustomLogger logger) : base(provider, logger)
        {
        }

        /// <inheritdoc/>
        public override async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                return await _dbSet.Where(x => !x.IsDeleted).Where(predicate).AnyAsync();
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<bool>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<TEntity> FindByAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = false,
            EntityLoader<TEntity> entityLoader = null,
            ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                // Building the query with the necessary includes.
                IQueryable<TEntity> query = _dbSet;

                //load includes if there's any
                if (entityLoader != null)
                    query = await entityLoader.LoadIncludesAsync(query);

                //apply filters
                query = query.Where(x => !x.IsDeleted).Where(predicate);

                // Applying NoTracking if requested.
                if (noTracking)
                    query = query.AsNoTracking();

                // Executing the query.
                var entity = await query.FirstOrDefaultAsync();

                //load explicits if there's any
                if (entityLoader != null)
                    await entityLoader.LoadExplicitsAsync(_provider.Context, entity);

                return entity;
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<TEntity>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                return await _dbSet.Where(x => !x.IsDeleted).CountAsync(predicate);
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<int>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<IList<TEntity>> ListByAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                if (noTracking)
                    return await _dbSet.AsNoTracking().Where(x => !x.IsDeleted).Where(predicate).ToListAsync();
                else
                    return await _dbSet.Where(x => !x.IsDeleted).Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<IList<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                if (noTracking)
                    return _dbSet.AsNoTracking().Where(x => !x.IsDeleted).Where(predicate);
                else
                    return _dbSet.Where(x => !x.IsDeleted).Where(predicate);
            }
            catch (Exception ex)
            {
                return FuncOnError<IQueryable<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override IQueryable<TEntity> GetBareQuery(bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                if (noTracking)
                    return _dbSet.AsNoTracking().Where(x => !x.IsDeleted);
                else
                    return _dbSet.Where(x => !x.IsDeleted);
            }
            catch (Exception ex)
            {
                return FuncOnError<IQueryable<TEntity>>(action, ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<PagedList<TEntity>> PagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, bool noTracking = false, ErrorHandlingOption action = ErrorHandlingOption.Throw)
        {
            try
            {
                if (pageNumber < minPageNumber) pageNumber = minPageNumber;
                if (pageSize > maxPageSize) pageSize = maxPageSize;

                IQueryable<TEntity> query;

                if (noTracking)
                    query = _dbSet.AsNoTracking().Where(x => !x.IsDeleted).Where(predicate);
                else
                    query = _dbSet.Where(x => !x.IsDeleted).Where(predicate);

                return await PagedList<TEntity>.ToPagedListAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                return await FuncOnErrorAsync<PagedList<TEntity>>(action, ex);
            }
        }
    }
}
