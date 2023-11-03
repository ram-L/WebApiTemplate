using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides extension methods to configure Swagger/OpenAPI documentation and UI.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configures Swagger/OpenAPI documentation generation.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration">The application's configuration.</param>
        /// <remarks>
        /// Adds Swagger generation services with XML comments if available.
        /// </remarks>
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration["Swagger:Title"] ?? "WebApiTemplate API",
                    Version = configuration["Swagger:Version"] ?? "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        /// <summary>
        /// Configures the Swagger/OpenAPI UI.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
        /// <param name="configuration">The application's configuration.</param>
        /// <remarks>
        /// Sets up the Swagger UI based on the provided configuration. If the 'Swagger:RoutePrefix' setting is provided in the configuration, it will be used as the route prefix. Otherwise, an empty string is used, making the Swagger UI available at the application's root.
        /// </remarks>
        public static void UseSwaggerUIConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var routePrefix = configuration["Swagger:RoutePrefix"] ?? string.Empty;
                var apiName = configuration["Swagger:Title"] ?? "WebApiTemplate API";
                var apiVersion = configuration["Swagger:Version"] ?? "V1";

                c.SwaggerEndpoint($"/swagger/{apiVersion.ToLower()}/swagger.json", $"{apiName} {apiVersion}");
                c.RoutePrefix = routePrefix;
            });
        }

    }

}
