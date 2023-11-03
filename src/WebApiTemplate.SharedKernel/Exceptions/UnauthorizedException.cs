namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when access to a resource is unauthorized.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a default message.
        /// </summary>
        public UnauthorizedException()
            : base("Unauthorized access.")
        {
        }
    }

}
