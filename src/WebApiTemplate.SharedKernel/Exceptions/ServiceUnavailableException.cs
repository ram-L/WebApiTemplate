namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a service is temporarily unavailable.
    /// </summary>
    public class ServiceUnavailableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class with the specified service name.
        /// </summary>
        /// <param name="serviceName">The name of the unavailable service.</param>
        public ServiceUnavailableException(string serviceName)
            : base($"{serviceName} is currently unavailable.")
        {
        }
    }

}
