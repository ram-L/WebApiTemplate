namespace WebApiTemplate.SharedKernel.Exceptions
{

    /// <summary>
    /// Represents an exception that occurs when an unexpected error occurs.
    /// </summary>
    public class UnexpectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the unexpected error.</param>
        public UnexpectedException(string message)
            : base($"An unexpected error occurred: {message}")
        {
        }
    }

}
