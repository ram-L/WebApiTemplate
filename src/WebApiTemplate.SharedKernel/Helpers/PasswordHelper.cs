using Microsoft.AspNetCore.Identity;

namespace WebApiTemplate.SharedKernel.Helpers
{
    /// <summary>
    /// Helper class for hashing passwords using ASP.NET Core Identity's PasswordHasher.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Hashes a password for a specified entity using ASP.NET Core Identity's PasswordHasher.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity associated with the password.</typeparam>
        /// <param name="entity">The entity for which the password is hashed.</param>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The hashed password as a string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="password"/> is null or empty.</exception>
        public static string HashPassword<TEntity>(TEntity entity, string password) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            var hasher = new PasswordHasher<TEntity>();
            var hashedPassword = hasher.HashPassword(entity, password);
            return hashedPassword;
        }

        public static bool VerifyPassword<TEntity>(TEntity entity, string hashedPassword, string providedPassword) where TEntity : class
        {
            var hasher = new PasswordHasher<TEntity>();
            var result = hasher.VerifyHashedPassword(entity, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
