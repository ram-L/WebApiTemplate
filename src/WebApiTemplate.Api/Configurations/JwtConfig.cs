using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiTemplate.Api.Authorization;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides methods for configuring JWT authentication.
    /// </summary>
    public static class JwtConfig
    {
        /// <summary>
        /// Configures JWT authentication for the web API using the provided <paramref name="configuration"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure services.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> containing JWT configuration settings.</param>
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind JwtAuthOptions from app settings
            var jwtOptions = new JwtAuthOptions();
            configuration.GetSection("Jwt").Bind(jwtOptions);

            // Now add JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    try
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuers = new List<string> { jwtOptions.Issuer },
                            ValidAudiences = new List<string> { jwtOptions.Audience },
                            IssuerSigningKey = jwtOptions.GetSecurityKey() // public key or if not, secret 
                        };
                    }
                    catch(Exception ex)
                    {

                    }                   
                });
        }
    }
}
