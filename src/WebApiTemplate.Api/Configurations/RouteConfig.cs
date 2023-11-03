namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides configuration for defining routes in the Web API application.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Configures the routes for controllers and actions.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> used to configure routes.</param>
        public static void ConfigureRoutes(this IEndpointRouteBuilder endpoints)
        {
            // Define your routes here. For example:
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
