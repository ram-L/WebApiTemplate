using Autofac;
using AutoMapper;
using System.Reflection;

namespace WebApiTemplate.Application.DependencyInjection
{
    /// <summary>
    /// Autofac module for configuring AutoMapper profiles.
    /// </summary>
    public class AutoMapperModule : Autofac.Module
    {
        private readonly Assembly[] _assembliesToScan;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperModule"/> class.
        /// </summary>
        /// <param name="assembliesToScan">The assemblies to scan for AutoMapper profiles.</param>
        public AutoMapperModule(params Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan ?? throw new ArgumentNullException(nameof(assembliesToScan));
        }

        /// <summary>
        /// Loads the AutoMapper configuration and registers profiles.
        /// </summary>
        /// <param name="builder">The Autofac container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            // Register all profile classes in the specified assemblies
            var profiles = _assembliesToScan
                .SelectMany(a => a.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic))
                .ToArray();

            builder.RegisterTypes(profiles).As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
