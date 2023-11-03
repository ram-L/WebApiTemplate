using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApiTemplate.Api.Authorization;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Models.Auth;
using WebApiTemplate.SharedKernel.Enums;
using System.Security.Claims;

namespace WebApiTemplate.Api.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtAuthOptions _options;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="options">The JWT authentication options.</param>
        /// <param name="authService">The authentication service.</param>
        /// <exception cref="ArgumentNullException">Thrown when options or authService is null.</exception>
        public AuthController(IOptions<JwtAuthOptions> options, IAuthService authService)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="dto">The login DTO containing user credentials.</param>
        /// <returns>An HTTP response with a JWT token on successful authentication.</returns>
        [HttpPost("login/user")]
        public async Task<IActionResult> LoginUser(UserLoginDto dto)
        {
            var user = await _authService.AuthenticateAsync(dto);

            var token = _authService.GenerateAuth(user, _options);
            return Ok(token);
        }

        /// <summary>
        /// Authenticates a client and generates a JWT token.
        /// </summary>
        /// <param name="dto">The client login DTO containing client credentials.</param>
        /// <returns>An HTTP response with a JWT token on successful client authentication.</returns>
        [HttpPost("login/client")]
        public async Task<IActionResult> LoginClient(ClientLoginDto dto)
        {
            var client = await _authService.AuthenticateAsync(dto);

            var token = _authService.GenerateAuth(client, _options);
            return Ok(token);
        }
    }
}
