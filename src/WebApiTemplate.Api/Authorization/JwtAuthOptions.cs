using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.SharedKernel.Exceptions;

namespace WebApiTemplate.Api.Authorization
{
    /// <summary>
    /// Represents the configuration options for JSON Web Token (JWT) authentication.
    /// </summary>
    public class JwtAuthOptions : IAuthOptions
    {
        private string _securityAlg = SecurityAlgorithms.RsaSha256;

        /// <summary>
        /// Gets or sets the JWT secret used for symmetric key signing.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the file path to the private key (for RSA signing).
        /// </summary>
        public string PrivateKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the file path to the public key (for RSA signing).
        /// </summary>
        public string PublicKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the JWT.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience of the JWT.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiration time for JWT tokens. Default is 1 hour.
        /// </summary>
        public TimeSpan ExpirationTime { get; set; } = TimeSpan.FromHours(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthOptions"/> class.
        /// </summary>
        public JwtAuthOptions() { }

        /// <summary>
        /// Gets the security key based on the configuration.
        /// </summary>
        /// <param name="isPrivate">A flag indicating whether to use the private key (for RSA signing).</param>
        /// <returns>The appropriate <see cref="SecurityKey"/> based on the configuration.</returns>
        /// <exception cref="InvalidOperationException">Thrown when JWT keys are not configured or the files do not exist.</exception>
        public SecurityKey GetSecurityKey(bool isPrivate = false)
        {
            if (!string.IsNullOrEmpty(PublicKeyPath) && File.Exists(PublicKeyPath) && !string.IsNullOrEmpty(PrivateKeyPath) && File.Exists(PrivateKeyPath))
            {
                string key = File.ReadAllText(isPrivate ? PrivateKeyPath : PublicKeyPath);
                var rsa = RSA.Create();
                rsa.ImportFromPem(key.ToCharArray());
                _securityAlg = SecurityAlgorithms.RsaSha256;
                return new RsaSecurityKey(rsa);
            }
            if (!string.IsNullOrEmpty(Secret))
            {
                _securityAlg = SecurityAlgorithms.HmacSha256;
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            }

            throw new ConfigurationException("Please check JWT configurations.");
        }

        /// <summary>
        /// Gets the security algorithm based on the configuration.
        /// </summary>
        /// <returns>The security algorithm as a string.</returns>
        public string GetSecurityAlg()
        {
            return _securityAlg;
        }
    }
}
