using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApiTemplate.SharedKernel.Exceptions;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IAuthOptions
    {
        /// <summary>
        /// Gets or sets the JWT secret used for symmetric key signing.
        /// </summary>
        string Secret { get; set; }

        /// <summary>
        /// Gets or sets the file path to the private key (for RSA signing).
        /// </summary>
        string PrivateKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the file path to the public key (for RSA signing).
        /// </summary>
        string PublicKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the JWT.
        /// </summary>
        string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience of the JWT.
        /// </summary>
        string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiration time for JWT tokens. Default is 1 hour.
        /// </summary>
        TimeSpan ExpirationTime { get; set; }

        /// <summary>
        /// Gets the security key based on the configuration.
        /// </summary>
        /// <param name="isPrivate">A flag indicating whether to use the private key (for RSA signing).</param>
        /// <returns>The appropriate <see cref="SecurityKey"/> based on the configuration.</returns>
        /// <exception cref="InvalidOperationException">Thrown when JWT keys are not configured or the files do not exist.</exception>
        SecurityKey GetSecurityKey(bool isPrivate = false);

        /// <summary>
        /// Gets the security algorithm based on the configuration.
        /// </summary>
        /// <returns>The security algorithm as a string.</returns>
        string GetSecurityAlg();
    }
}
