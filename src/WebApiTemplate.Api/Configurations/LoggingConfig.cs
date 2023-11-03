using Serilog;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides configuration for logging in the Web API application.
    /// </summary>
    public static class LoggingConfig
    {
        /// <summary>
        /// Configures logging using Serilog for the Web API application.
        /// </summary>
        /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure the application.</param>
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            // Configure Serilog
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            // Set up Serilog in the host
            builder.Host.UseSerilog(logger);

            // Clear default providers and add Serilog
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
    }

}
