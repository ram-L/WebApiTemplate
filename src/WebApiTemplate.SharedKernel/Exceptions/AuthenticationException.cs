using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when authentication fails.
    /// </summary>
    public class AuthenticationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationException"/> class with a default message.
        /// </summary>
        public AuthenticationException()
            : base("Authentication failed. Invalid Username or Password")
        {
        }

        public AuthenticationException(string message)
           : base($"Authentication failed: {message}")
        {
          
        }
    }
}
