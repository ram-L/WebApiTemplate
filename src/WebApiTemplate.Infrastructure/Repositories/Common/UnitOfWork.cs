using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories.Interfaces;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Infrastructure.Repositories.Common
{
    /// <summary>
    /// Represents a unit of work that can be used to group one or more operations into a single transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IAppDbContextProvider _provider;
        protected IDbContextTransaction _transaction;
        protected readonly ICustomLogger _logger;

        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="provider">The provider for accessing the application's database context.</param>
        /// <param name="logger">The logger for capturing log messages.</param>
        public UnitOfWork(IAppDbContextProvider provider, ICustomLogger logger)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task BeginTransactionAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (_transaction == null)
            {
                _transaction = await _provider.Context.Database.BeginTransactionAsync();
            }
        }

        /// <inheritdoc/>
        public async Task CommitAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            try
            {
                await _provider.Context.SaveChangesAsync();
                await _transaction?.CommitAsync();
            }
            finally
            {
                if (_transaction != null)
                    await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <inheritdoc/>
        public async Task RollbackAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (_transaction != null)
            {
                await _transaction?.RollbackAsync();
                if (_transaction != null)
                    await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <inheritdoc/>
        public async Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _provider.Context.Set<TEntity>().AddAsync(entity);
            await _provider.Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var entry = _provider.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _provider.Context.Set<TEntity>().Attach(entity);
                entry.State = EntityState.Modified;
            }
            await _provider.Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _provider.Context.Set<TEntity>().Remove(entity);
            await _provider.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UnitOfWork"/> and optionally
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _provider.Context?.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
