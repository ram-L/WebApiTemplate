using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiTemplate.Api.Authorization;
using WebApiTemplate.SharedKernel.Constants;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Api.Middlewares
{
    /// <summary>
    /// Middleware for JWT authentication in the API.
    /// </summary>
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtAuthOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next request delegate in the pipeline.</param>
        /// <param name="options">The JWT authentication options.</param>
        public JwtAuthenticationMiddleware(
            RequestDelegate next,
            IOptions<JwtAuthOptions> options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Invokes the middleware to handle JWT authentication.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetRequiredService<ICustomLogger>();

            if (TryGetToken(context, out var token))
            {
                if (ValidateToken(token, logger, out var validatedToken))
                {
                    var claims = ((JwtSecurityToken)validatedToken).Claims.ToList();

                    var permissionsClaim = claims.FirstOrDefault(c => c.Type == UserClaimTypes.Permissions);
                    if (permissionsClaim != null)
                    {
                        var identity = new ClaimsIdentity(claims, "jwt");
                        var principal = new ClaimsPrincipal(identity);
                        context.User = principal;

                        logger.LogMessage("JWT token validated successfully", LogEventLevel.Information);
                    }
                    else
                    {
                        logger.LogMessage("No permissions found", LogEventLevel.Warning);
                        context.User = new ClaimsPrincipal(new ClaimsIdentity()); // Set to unauthenticated principal
                    }
                }
                else
                {
                    logger.LogMessage("Invalid JWT token provided", LogEventLevel.Warning);
                    context.User = new ClaimsPrincipal(new ClaimsIdentity()); // Set to unauthenticated principal
                }
            }

            await _next(context);
        }

        private bool TryGetToken(HttpContext context, out string token)
        {
            token = null;

            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                return false;
            }

            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            token = authorizationHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            return true;
        }

        private bool ValidateToken(string token, ICustomLogger logger, out SecurityToken validatedToken)
        {
            validatedToken = null;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _options.GetSecurityKey(),
                    ValidateIssuer = true,  // Set to true if you want to validate the issuer
                    ValidateAudience = true, // Set to true if you want to validate the audience
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidIssuers = new List<string> { _options.Issuer },
                    ValidAudiences = new List<string> { _options.Audience },
                }, out validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogMessage("Token validation failed", LogEventLevel.Error, ex);
                return false;
            }
        }
    }
}
