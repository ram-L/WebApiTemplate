using AutoMapper;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.SharedKernel.Exceptions;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Application.Services
{
    /// <summary>
    /// Base class for services providing common functionalities.
    /// </summary>
    public abstract class ServiceBase : IServiceBase
    {
        protected readonly ICurrentUserContext _currentUser;
        protected readonly ICustomLogger _logger;
        protected readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        protected ServiceBase(ICurrentUserContext currentUser, ICustomLogger logger, IMapper mapper)
        {
            _currentUser = currentUser ?? throw new UnauthorizedException();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Queries to paged result.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="options">The OData query options.</param>
        /// <param name="query">The query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the paged result.</returns>
        protected virtual async Task<PagedResult<T>> QueryToPagedResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> query)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (query == null) throw new ArgumentNullException(nameof(query));

            // Apply OData queries
            var totalCount = await GetQueryTotalAsync(options, query).ConfigureAwait(false);
            var results = options.ApplyTo(query) as IQueryable<T>;
            return await ToPagedResultAsync(options, results, totalCount).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the query total.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="options">The OData query options.</param>
        /// <param name="query">The query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count of items.</returns>
        protected virtual async Task<int> GetQueryTotalAsync<T>(ODataQueryOptions<T> options, IQueryable<T> query)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (query == null) throw new ArgumentNullException(nameof(query));

            //ignore $top and $skip to get count
            var ignoreQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top;
            var filteredResults = options.ApplyTo(query, ignoreQueryOptions) as IQueryable<T> ?? query;
            return await filteredResults.CountAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Converts the query to a paged result.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="options">The OData query options.</param>
        /// <param name="query">The query.</param>
        /// <param name="totalCount">The total count of items.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the paged result.</returns>
        protected virtual async Task<PagedResult<T>> ToPagedResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> query, int totalCount)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (query == null || totalCount == 0)
            {
                // Return an empty list or throw an exception
                return new PagedResult<T>
                {
                    Data = Enumerable.Empty<T>(),
                    CurrentPage = 0,
                    PageSize = 0,
                    TotalCount = 0,
                    TotalPages = 0
                };
            }

            var dataList = await query.ToListAsync().ConfigureAwait(false);

            var pageSize = options.Top?.Value ?? totalCount; // If $top is not set, use totalCount
            var skip = options.Skip?.Value ?? 0;
            var currentPage = (skip / pageSize) + 1;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<T>
            {
                Data = dataList,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }
    }
}
