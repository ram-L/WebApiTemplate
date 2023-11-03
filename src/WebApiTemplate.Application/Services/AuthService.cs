using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Models.Auth;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Repositories.Interfaces;
using WebApiTemplate.SharedKernel.Constants;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Exceptions;
using WebApiTemplate.SharedKernel.Extensions;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Services
{
    /// <summary>
    /// Service for handling authentication-related operations.
    /// </summary>
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRoleRepository _userRoleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="currentUser">The current user context.</param>
        /// <param name="logger">The custom logger.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when userRepository or userRoleRepository is null.</exception>
        public AuthService(
            IUserRepository userRepository,
            IAccountRoleRepository userRoleRepository,
            ICurrentUserContext currentUser,            
            ICustomLogger logger,
            IMapper mapper)
            : base(currentUser, logger, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        }

        /// <inheritdoc />
        public async Task<AuthenticatedUser> AuthenticateAsync(UserLoginDto loginDto)
        {
            // Eager load needed entities for password check
            var entityLoader = new EntityLoader<Account>();
            entityLoader.AddIncludes(q => q.Include(u => u.UserProfile));

            var user = await _userRepository.FindByAsync(x =>
                x.AccountType == AccountType.User &&
                x.UserProfile.Username == loginDto.Username,
                entityLoader: entityLoader);

            if (user == null)
                throw new AuthenticationException();

            if (user.Status != AccountStatus.Active)
                throw new AuthenticationException(user.Status.GetAccountMessage());

            var userPasswordHash = user.UserProfile.PasswordHash;
            if (!PasswordHelper.VerifyPassword(user, userPasswordHash, loginDto.Password))
                throw new AuthenticationException();

            // Load the other entities explicitly
            await _userRepository.LoadExplicitsAsync(user, u => u.Roles);
            await _userRoleRepository.LoadExplicitsAsync(user.Roles, u => u.Role, u => u.Role.Claims);
            var authUser = new AuthenticatedUser
            {
                UserId = user.Id,
                AccountType = user.AccountType,
                Username = user.UserProfile.Username,
                Email = user.UserProfile?.Email
            };

            authUser.Permissions = GetPermissions(user?.Roles);

            return authUser;
        }

        /// <inheritdoc />
        public async Task<AuthenticatedClient> AuthenticateAsync(ClientLoginDto clientDto)
        {
            // Eager load needed entities for client check
            var entityLoader = new EntityLoader<Account>();
            entityLoader.AddIncludes(q => q.Include(u => u.ClientProfile));

            var client = await _userRepository.FindByAsync(x =>
                x.AccountType == AccountType.Client &&
                x.ClientProfile.ClientKey == clientDto.ClientKey,
                entityLoader: entityLoader);

            if (client == null)
                throw new AuthenticationException("Invalid Client Key");

            if (client.Status != AccountStatus.Active)
                throw new AuthenticationException(client.Status.GetAccountMessage());

            // Load the other entities explicitly
            await _userRepository.LoadExplicitsAsync(client, u => u.Roles);
            await _userRoleRepository.LoadExplicitsAsync(client.Roles, u => u.Role, u => u.Role.Claims);
            var authClient = new AuthenticatedClient
            {
                ClientId = client.Id,
                AccountType = client.AccountType,
                ClientName = client.ClientProfile.ClientName,
                Description = client.ClientProfile.Description
            };
            
            authClient.Permissions = GetPermissions(client?.Roles);

            return authClient;
        }       

        /// <inheritdoc />
        public AuthResponseDto GenerateAuth(AuthenticatedUser user, IAuthOptions authOptions)
        {
            // Generate an authentication token using the provided parameters
            var authToken = GenerateUserAuthToken(user, authOptions);

            // var refreshToken = GenerateRefreshToken();

            var permissions = user.Permissions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ConvertToString());

            // Create and return the authentication response
            return new AuthResponseDto
            {
                AccessToken = authToken,
                RefreshToken = "", // Refresh token generation logic can be added here
                Permissions = permissions
            };
        }

        /// <inheritdoc />
        public AuthResponseDto GenerateAuth(AuthenticatedClient client, IAuthOptions authOptions)
        {
            // Generate an authentication token using the provided parameters
            var authToken = GenerateClientAuthToken(client, authOptions);

            // var refreshToken = GenerateRefreshToken();

            var permissions = client.Permissions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ConvertToString());

            // Create and return the authentication response
            return new AuthResponseDto
            {
                AccessToken = authToken,
                RefreshToken = "", // Refresh token generation logic can be added here
                Permissions = permissions
            };
        }

        private Dictionary<PermissionResource, PermissionType> GetPermissions(IEnumerable<AccountRole> accountRoles)
        {
            // Merge role claims since it's possible the user has multiple roles
            var roleClaims = accountRoles?.SelectMany(x => x.Role?.Claims?.Select(y => y));
            return roleClaims.GroupBy(r => r.Resource)
                .ToDictionary(
                    g => g.Key,
                    g => g.Aggregate(PermissionType.None, (acc, role) => acc | role.Permission)
                );
        }

        private string GenerateUserAuthToken(AuthenticatedUser user, IAuthOptions authOptions)
        {
            var credentials = new SigningCredentials(authOptions.GetSecurityKey(true), authOptions.GetSecurityAlg());

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(UserClaimTypes.AccountType, user.AccountType.ToString()),
                new Claim(UserClaimTypes.UserId, user.UserId.ToString()),
                new Claim(UserClaimTypes.Permmisions, JsonConvert.SerializeObject(user.Permissions))
            };

            var token = new JwtSecurityToken(authOptions.Issuer, authOptions.Audience, claims,
                expires: DateTime.Now.AddSeconds(authOptions.ExpirationTime.TotalSeconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateClientAuthToken(AuthenticatedClient client, IAuthOptions authOptions)
        {
            var credentials = new SigningCredentials(authOptions.GetSecurityKey(true), authOptions.GetSecurityAlg());

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClientClaimTypes.AccountType, client.AccountType.ToString()),
                new Claim(ClientClaimTypes.ClientType, client.ClientType.ToString()),
                new Claim(ClientClaimTypes.ClientId, client.ClientId.ToString()),
                new Claim(ClientClaimTypes.ClientName, client.ClientName),
                new Claim(UserClaimTypes.Permmisions, JsonConvert.SerializeObject(client.Permissions))
            };

            var token = new JwtSecurityToken(authOptions.Issuer, authOptions.Audience, claims,
                expires: DateTime.Now.AddSeconds(authOptions.ExpirationTime.TotalSeconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
