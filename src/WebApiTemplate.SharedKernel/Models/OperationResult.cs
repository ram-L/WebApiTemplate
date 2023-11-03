using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTemplate.SharedKernel.Models
{
    /// <summary>
    /// Represents the result of an operation, which can be either a success or a failure.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets an optional error message associated with the failure, or null for success.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Gets a value indicating whether the operation result represents a failure.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="isSuccess">A boolean indicating whether the operation is successful.</param>
        /// <param name="error">An optional error message for failure cases (null for success).</param>
        protected OperationResult(bool isSuccess, string error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("Invalid operation result. Error message should be null for a successful result.");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Invalid operation result. Error message cannot be null for a failure result.");

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Creates a new successful operation result.
        /// </summary>
        /// <returns>A successful operation result.</returns>
        public static OperationResult Success()
        {
            return new OperationResult(true, null);
        }

        /// <summary>
        /// Creates a new failure operation result with an error message.
        /// </summary>
        /// <param name="error">The error message for the failure.</param>
        /// <returns>A failure operation result with the specified error message.</returns>
        public static OperationResult Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
                throw new ArgumentNullException(nameof(error), "Error message cannot be null or empty for a failure result.");

            return new OperationResult(false, error);
        }
    }

    /// <summary>
    /// Represents the result of an operation that produces a value of type T.
    /// </summary>
    /// <typeparam name="T">The type of the value produced by the operation.</typeparam>
    public class Result<T> : OperationResult
    {
        /// <summary>
        /// Gets the value produced by the operation if successful, or the default value for type T for failures.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value produced by the operation.</param>
        /// <param name="isSuccess">A boolean indicating whether the operation is successful.</param>
        /// <param name="error">An optional error message for failure cases (null for success).</param>
        private Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new successful operation result with a value of type T.
        /// </summary>
        /// <param name="value">The value produced by the operation.</param>
        /// <returns>A successful operation result with the specified value.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        /// <summary>
        /// Creates a new failure operation result with an error message.
        /// </summary>
        /// <param name="error">The error message for the failure.</param>
        /// <returns>A failure operation result with the specified error message.</returns>
        public static new Result<T> Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
                throw new ArgumentNullException(nameof(error), "Error message cannot be null or empty for a failure result.");

            return new Result<T>(default, false, error);
        }
    }
}
