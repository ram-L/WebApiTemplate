namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when there's an error in configuration.
    /// </summary>
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class with a default message.
        /// </summary>
        public ConfigurationException()
            : base("There's an error in configuration")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class with a custom message.
        /// </summary>
        /// <param name="message">The custom message describing the configuration error.</param>
        public ConfigurationException(string message)
            : base($"Configuration error: {message}")
        {

        }
    }
}
