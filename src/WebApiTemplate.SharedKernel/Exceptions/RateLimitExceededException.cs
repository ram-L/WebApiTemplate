namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a rate limit is exceeded for a specific action.
    /// </summary>
    public class RateLimitExceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitExceededException"/> class with the specified action name.
        /// </summary>
        /// <param name="action">The name of the action for which the rate limit was exceeded.</param>
        public RateLimitExceededException(string action)
            : base($"Rate limit exceeded for {action}.")
        {
        }
    }

}
