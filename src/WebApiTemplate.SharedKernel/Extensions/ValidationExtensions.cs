using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Models;
using ValidationException = WebApiTemplate.SharedKernel.Exceptions.ValidationException;

namespace WebApiTemplate.Application.Extensions
{
    /// <summary>
    /// Extension methods for performing validation with FluentValidation and handling validation exceptions.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validates an instance of type <typeparamref name="T"/> using the provided <paramref name="validator"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to validate.</typeparam>
        /// <param name="validator">The validator to use for validation.</param>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="resourceName">The resource name associated with the validation errors.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous validation operation.</returns>
        public static async Task ValidateAsync<T>(this IValidator<T> validator, T instance, string resourceName) where T : class
        {
            var validationResult = await validator.ValidateAsync(instance);
            if (!validationResult.IsValid)
            {
                ThrowValidationException(validationResult, resourceName);
            }
        }

        /// <summary>
        /// Asynchronously validates a collection of validators encapsulated within a <see cref="CompositeValidator"/>. 
        /// If any of the validations fail, a <see cref="ValidationException"/> is thrown, encapsulating all the validation errors.
        /// </summary>
        /// <param name="compositeValidator">The composite validator containing a collection of validators to be validated.</param>
        /// <param name="resourceName">The resource name to be associated with any validation errors, aiding in identifying the source of the errors.</param>
        /// <param name="validators">An array of <see cref="ValidatorContextPair"/> objects, each representing a validator and its associated validation context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous validation operation.</returns>
        /// <exception cref="ValidationException">Thrown when the validation fails, containing details about all the validation errors across all validators.</exception>

        public static async Task ValidateAsync(this CompositeValidator compositeValidator, string resourceName, params ValidatorContextPair[] validators)
        {
            foreach (var validator in validators)
            {
                compositeValidator.AddValidator(validator);
            }

            var validationResult = await compositeValidator.ValidateAsync();
            if (!validationResult.IsValid)
            {
                ThrowValidationException(validationResult, resourceName);
            }
        }

        /// <summary>
        /// Converts the validation errors from a <see cref="ValidationResult"/> into a <see cref="ValidationException"/> and throws it.
        /// This method is used internally to standardize the exception throwing process for validation errors.
        /// </summary>
        /// <param name="validationResult">The result of the validation containing any errors.</param>
        /// <param name="resourceName">The resource name to be associated with the validation errors.</param>
        /// <exception cref="ValidationException">The exception that encapsulates the validation errors.</exception>

        private static void ThrowValidationException(ValidationResult validationResult, string resourceName)
        {
            var errors = validationResult.Errors.Select(x => new ErrorDetail
            {
                ResourceName = resourceName,
                PropertyName = x.PropertyName,
                ErrorCode = x.ErrorCode,
                ErrorMessage = x.ErrorMessage
            }).ToList();

            throw new ValidationException(errors);
        }
    }
}
