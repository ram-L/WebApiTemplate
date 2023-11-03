using Autofac;
using FluentValidation;
using System.Reflection;

namespace WebApiTemplate.Application.DependencyInjection
{
    /// <summary>
    /// Autofac module for registering FluentValidation validators.
    /// </summary>
    public class FluentValidationModule : Autofac.Module
    {
        private readonly Assembly[] _assembliesToScan;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationModule"/> class.
        /// </summary>
        /// <param name="assembliesToScan">The assemblies to scan for FluentValidation validators.</param>
        public FluentValidationModule(params Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan ?? throw new ArgumentNullException(nameof(assembliesToScan));
        }

        /// <summary>
        /// Loads the FluentValidation validators and registers them with Autofac.
        /// </summary>
        /// <param name="builder">The Autofac container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            // Register all validator types in the specified assemblies
            var validatorTypes = _assembliesToScan
                .SelectMany(a => a.GetTypes().Where(t => t.IsClosedTypeOf(typeof(IValidator<>)) && !t.IsAbstract && t.IsPublic))
                .ToArray();

            foreach (var validatorType in validatorTypes)
            {
                builder.RegisterType(validatorType).AsImplementedInterfaces().InstancePerDependency();
            }
        }
    }

    /// <summary>
    /// Extension methods for Type objects.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if a type is a closed generic type of a specified open generic type.
        /// </summary>
        /// <param name="type">The Type object to check.</param>
        /// <param name="openGenericType">The open generic type to compare against.</param>
        /// <returns>True if the type is a closed generic type of the specified open generic type; otherwise, false.</returns>
        public static bool IsClosedTypeOf(this Type type, Type openGenericType)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType);
        }
    }
}
