using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.SharedKernel.Models
{
    /// <summary>
    /// Represents details of an individual error.
    /// </summary>
    public class ErrorDetail
    {
        public string ResourceName { get; set; }
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the error code or identifier for the specific error type.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the human-readable error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets additional details or the exception message.
        /// </summary>
        public string Details { get; set; }

        public ErrorDetail()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDetail"/> class.
        /// </summary>
        /// <param name="errorCode">The error code or identifier for the specific error type.</param>
        /// <param name="errorMessage">The human-readable error message.</param>
        /// <param name="details">Additional details or the exception message.</param>
        public ErrorDetail(string errorCode, string errorMessage, string details = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details;
        }
    }

}
