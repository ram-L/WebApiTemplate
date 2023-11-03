namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a database operation fails.
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the database operation failure.</param>
        public DatabaseException(string message)
            : base(message)
        {
        }
    }

}
