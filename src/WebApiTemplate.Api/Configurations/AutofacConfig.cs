using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides configuration for integrating Autofac as the DI container.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Configures Autofac as the default service provider and registers custom modules.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> used to configure the application.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void ConfigureAutofac(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            // Use Autofac as the default service provider
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Configure Autofac
            hostBuilder.ConfigureContainer<ContainerBuilder>(builder => {
                builder.RegisterModule(new SharedKernel.DependencyInjection.AutofacModule());
                builder.RegisterModule(new Infrastructure.DependencyInjection.AutofacModule());
                builder.RegisterModule(new Application.DependencyInjection.AutofacModule());
                builder.RegisterModule(new Api.DependencyInjection.AutofacModule());
            });
        }
    }
}
