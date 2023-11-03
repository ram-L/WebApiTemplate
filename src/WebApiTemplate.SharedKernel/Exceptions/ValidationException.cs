using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when input validation fails.
    /// </summary>
    public class ValidationException : Exception, IExceptionError
    {
        /// <summary>
        /// Gets the list of error details associated with the validation exception.
        /// </summary>
        public List<ErrorDetail> Errors { get; private set; } = new List<ErrorDetail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specific error message.
        /// </summary>
        /// <param name="message">The error message that explains why the input data is invalid.</param>
        public ValidationException(string message)
            : base($"Invalid input data: {message}")
        {
            // Adding the error detail to the Errors list.
            Errors.Add(new ErrorDetail
            {
                ErrorCode = $"{ErrorCode.InvalidInput}",
                ErrorMessage = Message
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a single <see cref="ErrorDetail"/>.
        /// </summary>
        /// <param name="error">The <see cref="ErrorDetail"/> object representing the validation error.</param>
        public ValidationException(ErrorDetail error)
            : base(null)
        {
            // Adding the error detail to the Errors list.
            if (error != null)
            {
                Errors.Add(error);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a list of <see cref="ErrorDetail"/>.
        /// </summary>
        /// <param name="errors">The list of <see cref="ErrorDetail"/> objects representing the validation errors.</param>
        public ValidationException(List<ErrorDetail> errors)
            : base(null)
        {
            if (errors != null)
            {
                Errors.AddRange(errors);
            }
        }
    }
}
