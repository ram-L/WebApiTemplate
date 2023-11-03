using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace WebApiTemplate.SharedKernel.Helpers
{
    /// <summary>
    /// A utility class for managing eager loading of related entities and explicit loading of navigation properties.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being loaded.</typeparam>
    public class EntityLoader<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets a list of expressions that specify the related entities to be eagerly loaded when querying the database.
        /// </summary>
        /// <remarks>
        /// Eager loading allows fetching related entities in a single query, optimizing performance by reducing the number of database round-trips.
        /// </remarks>
        public List<Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>> Includes { get; private set; }


        /// <summary>
        /// Gets or sets a list of expressions that specify the related entities to be explicitly loaded.
        /// </summary>
        public List<Expression<Func<TEntity, object>>> Explicits { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader{TEntity}"/> class.
        /// </summary>
        public EntityLoader()
        {
            Includes = new List<Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>>();
            Explicits = new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// Adds expressions to the Includes list for eager loading of related entities.
        /// </summary>
        /// <param name="includes">The expressions for related entities to be eagerly loaded.</param>
        public void AddIncludes(params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] includes)
        {
            if (Includes == null) Includes = new List<Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>>();
            Includes.AddRange(includes);
        }

        /// <summary>
        /// Adds expressions to the Explicits list for explicit loading of related entities.
        /// </summary>
        /// <param name="explicits">The expressions for related entities to be explicitly loaded.</param>
        public void AddExplicits(params Expression<Func<TEntity, object>>[] explicits)
        {
            if (Explicits == null) Explicits = new List<Expression<Func<TEntity, object>>>();
            Explicits.AddRange(explicits);
        }

        /// <summary>
        /// Loads related entities specified by the <see cref="Includes"/> property into the provided query asynchronously.
        /// </summary>
        /// <param name="query">The query to which related entities should be included.</param>
        /// <returns>An asynchronous task that represents the loading operation and returns the modified query.</returns>
        public async Task<IQueryable<TEntity>> LoadIncludesAsync(IQueryable<TEntity> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (Includes == null) return query;

            foreach (var include in Includes)
                if (include != null)
                    query = include.Compile().Invoke(query);

            return query;
        }

        /// <summary>
        /// Loads explicit navigation properties for the provided entity asynchronously.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> used for loading.</param>
        /// <param name="entity">The entity for which explicit navigation properties should be loaded.</param>
        /// <remarks>
        /// Materialize the query first, and this method then loads explicit navigation properties specified by the <see cref="Explicits"/> property.
        /// </remarks>
        public async Task LoadExplicitsAsync(DbContext context, TEntity entity)
        {
            if (entity == null)
                return; // Nothing to load, return early

            if (Explicits == null || !Explicits.Any())
                return; // Nothing to load, return early

            if (context == null)
                throw new ArgumentNullException(nameof(context), "DbContext cannot be null");

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
                throw new InvalidOperationException($"EntityType {typeof(TEntity).Name} not found in DbContext");

            foreach (var navigationProperty in Explicits)
            {
                if (navigationProperty == null)
                    continue; // Skip invalid navigation property names

                await LoadNavigationPropertyAsync(context, entity, navigationProperty, entityType);
            }
        }

        /// <summary>
        /// Asynchronously loads a navigation property for the specified entity based on the provided include expression.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> instance.</param>
        /// <param name="entity">The entity for which to load the navigation property.</param>
        /// <param name="include">An expression specifying the navigation property to load.</param>
        /// <param name="entityType">The <see cref="IEntityType"/> of the entity.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task LoadNavigationPropertyAsync(
            DbContext context,
            TEntity entity,
            Expression<Func<TEntity, object>> include,
            IEntityType entityType)
        {
            var memberExpressions = new Stack<MemberExpression>();
            var expression = include.Body;

            // Traverse the expression tree to identify the navigation path
            while (expression is MemberExpression memberExpression)
            {
                memberExpressions.Push(memberExpression);
                expression = memberExpression.Expression;
            }

            await LoadNavigationPropertyAsync(context, entity, memberExpressions, entityType);
        }

        /// <summary>
        /// Asynchronously loads a related collection or reference navigation property for a given entity.
        /// </summary>
        /// <param name="context">The DbContext to use for loading.</param>
        /// <param name="entity">The entity for which to load the navigation property.</param>
        /// <param name="memberExpressions">A stack of MemberExpressions representing the navigation path.</param>
        /// <param name="entityType">The EntityType of the entity.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task LoadNavigationPropertyAsync(
            DbContext context,
            object entity,
            Stack<MemberExpression> memberExpressions,
            IEntityType entityType)
        {
            if (memberExpressions.Count == 0 || entity == null) return; // No more navigation properties to load or entity is null, return early

            var memberExpression = memberExpressions.Pop();
            var propertyName = memberExpression.Member.Name;
            var navigation = entityType.FindNavigation(propertyName);

            if (navigation == null) return; // Skip loading if the navigation property is not found in the entity's model                                            

            if (navigation.IsCollection)
            {
                // Load a collection navigation property asynchronously
                var collection = context.Entry(entity).Collection(propertyName);
                await collection.LoadAsync();
            }
            else
            {
                var reference = context.Entry(entity).Reference(propertyName);
                if (reference.CurrentValue != null)
                {
                    // Load a reference navigation property asynchronously and recursively load nested properties
                    var nestedEntityType = context.Model.FindEntityType(reference.CurrentValue.GetType());
                    await LoadNavigationPropertyAsync(context, reference.CurrentValue, memberExpressions, nestedEntityType);
                }
                else
                {
                    await reference.LoadAsync();

                    var nestedEntity = reference.CurrentValue;
                    if (nestedEntity != null)
                    {
                        // If the reference has been loaded, recursively load its properties
                        var nestedEntityType = context.Model.FindEntityType(nestedEntity.GetType());
                        await LoadNavigationPropertyAsync(context, nestedEntity, memberExpressions, nestedEntityType);
                    }
                }
            }
        }
    }
}
