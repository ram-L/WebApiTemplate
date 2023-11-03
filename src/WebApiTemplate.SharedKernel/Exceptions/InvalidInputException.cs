using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when input data is invalid.
    /// This exception class extends the base Exception class and implements the IExceptionError interface.
    /// It is designed to provide a more detailed description of input validation errors.
    /// </summary>
    public class InvalidInputException : Exception, IExceptionError
    {
        /// <summary>
        /// Gets the list of <see cref="ErrorDetail"/> objects that describe the validation errors.
        /// This property is read-only to ensure that the error details are only modified through the constructor.
        /// </summary>
        public List<ErrorDetail> Errors { get; private set; } = new List<ErrorDetail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class with a specific error message.
        /// </summary>
        /// <param name="message">The error message that explains why the input data is invalid.</param>
        public InvalidInputException(string message)
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
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class with a single <see cref="ErrorDetail"/>.
        /// </summary>
        /// <param name="error">The <see cref="ErrorDetail"/> object representing the validation error.</param>
        public InvalidInputException(ErrorDetail error)
           : base(null)
        {
            // Adding the error detail to the Errors list.
            if (error != null)
            {
                Errors.Add(error);
            }           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class with a list of <see cref="ErrorDetail"/>.
        /// </summary>
        /// <param name="errors">The list of <see cref="ErrorDetail"/> objects representing the validation errors.</param>
        public InvalidInputException(List<ErrorDetail> errors)
           : base(null)
        {
            if (errors != null)
            {
                Errors.AddRange(errors);
            }
        }
    }

}
