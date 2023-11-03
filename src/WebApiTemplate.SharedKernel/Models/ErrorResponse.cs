namespace WebApiTemplate.SharedKernel.Models
{
    /// <summary>
    /// Represents an error response to be returned by a web API or application.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code associated with the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a list of error details.
        /// </summary>
        public List<ErrorDetail> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code associated with the error.</param>
        /// <param name="errors">A list of error details.</param>
        public ErrorResponse(int statusCode, List<ErrorDetail> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
