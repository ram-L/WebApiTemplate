using Microsoft.IdentityModel.Tokens;
using Microsoft.OData;
using WebApiTemplate.Application.Interfaces.Api;
using WebApiTemplate.Application.Models.Auth;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Application.Interfaces.Application
{
    /// <summary>
    /// Provides an interface for authentication-related operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user asynchronously.
        /// </summary>
        /// <param name="loginDto">The user login data transfer object.</param>
        /// <returns>An AuthenticatedUser instance.</returns>
        /// <exception cref="AuthenticationException">Thrown when authentication fails.</exception>
        Task<AuthenticatedUser> AuthenticateAsync(UserLoginDto loginDto);

        /// <summary>
        /// Authenticates a client asynchronously.
        /// </summary>
        /// <param name="clientDto">The client login data transfer object.</param>
        /// <returns>An AuthenticatedClient instance.</returns>
        /// <exception cref="AuthenticationException">Thrown when authentication fails.</exception>
        Task<AuthenticatedClient> AuthenticateAsync(ClientLoginDto clientDto);

        /// <summary>
        /// Generates an authentication response for a user.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <param name="authOptions">The authentication options.</param>
        /// <returns>An AuthResponseDto instance containing the token and permissions.</returns>
        AuthResponseDto GenerateAuth(AuthenticatedUser user, IAuthOptions authOptions);

        /// <summary>
        /// Generates an authentication response for a client.
        /// </summary>
        /// <param name="client">The authenticated client.</param>
        /// <param name="authOptions">The authentication options.</param>
        /// <returns>An AuthResponseDto instance containing the token and permissions.</returns>
        AuthResponseDto GenerateAuth(AuthenticatedClient client, IAuthOptions authOptions);
    }
}
