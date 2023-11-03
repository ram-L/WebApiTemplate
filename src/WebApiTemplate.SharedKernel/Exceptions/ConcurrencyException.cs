namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a concurrency conflict is detected during an update operation.
    /// </summary>
    public class ConcurrencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class with the specified resource name.
        /// </summary>
        /// <param name="resourceName">The name of the resource where the concurrency conflict occurred.</param>
        public ConcurrencyException(string resourceName)
            : base($"Concurrency conflict when updating {resourceName}.")
        {
        }
    }

}
