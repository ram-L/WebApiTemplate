using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.SharedKernel.Interfaces
{
    /// <summary>
    /// Represents an interface for objects that can provide a list of error details.
    /// </summary>
    public interface IExceptionError
    {
        /// <summary>
        /// Gets the list of error details.
        /// </summary>
        List<ErrorDetail> Errors { get; }
    }
}
