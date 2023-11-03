using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.SharedKernel.Helpers
{
    /// <summary>
    /// Represents a composite validator that combines multiple validators and their validation contexts.
    /// </summary>
    public class CompositeValidator
    {
        private readonly List<ValidatorContextPair> _validators = new();
        private readonly ValidatorContextPairValidator _validatorContextPairValidator = new();

        /// <summary>
        /// Adds a validator along with its validation context to the composite validator.
        /// </summary>
        /// <param name="validator">The validator to be added.</param>
        /// <param name="context">The validation context associated with the validator.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="validator"/> or <paramref name="context"/> is null.</exception>
        public void AddValidator(IValidator validator, IValidationContext context)
        {
            var validatorContextPair = new ValidatorContextPair(validator, context);
            _validatorContextPairValidator.ValidateAndThrow(validatorContextPair);
            _validators.Add(validatorContextPair);
        }

        /// <summary>
        /// Adds a <see cref="ValidatorContextPair"/> to the composite validator.
        /// </summary>
        /// <param name="validator">The validator context pair to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="validator"/> is null.</exception>
        public void AddValidator(ValidatorContextPair validator)
        {
            _validatorContextPairValidator.ValidateAndThrow(validator);
            _validators.Add(validator);
        }

        /// <summary>
        /// Adds multiple validators encapsulated in <see cref="ValidatorContextPair"/> instances to the composite validator.
        /// </summary>
        /// <param name="validators">A collection of validator context pairs to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="validators"/> is null.</exception>
        public void AddValidators(IEnumerable<ValidatorContextPair> validators)
        {
            if (validators == null) throw new ArgumentNullException(nameof(validators));

            foreach (var validatorContextPair in validators)
            {
                _validatorContextPairValidator.ValidateAndThrow(validatorContextPair);
                _validators.Add(validatorContextPair);
            }
        }

        /// <summary>
        /// Validates all added validators synchronously and returns the combined result.
        /// </summary>
        /// <returns>The combined validation result.</returns>
        public ValidationResult Validate()
        {
            var errors = _validators
                .SelectMany(vcp => vcp.Validator.Validate(vcp.Context).Errors)
                .ToList();

            return new ValidationResult(errors);
        }

        /// <summary>
        /// Validates all added validators asynchronously and returns the combined result.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation (optional).</param>
        /// <returns>The combined validation result.</returns>
        public async Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default)
        {
            var errors = (await Task.WhenAll(
                    _validators.Select(async vcp =>
                        new
                        {
                            vcp.Validator,
                            (await vcp.Validator.ValidateAsync(vcp.Context, cancellationToken).ConfigureAwait(false)).Errors
                        }))
                )
                .SelectMany(vcp => vcp.Errors)
                .ToList();

            return new ValidationResult(errors);
        }
    }

    /// <summary>
    /// Represents a validator for <see cref="ValidatorContextPair"/> to ensure that both the validator and the validation context are not null.
    /// </summary>
    public class ValidatorContextPairValidator : AbstractValidator<ValidatorContextPair>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorContextPairValidator"/> class.
        /// </summary>
        public ValidatorContextPairValidator()
        {
            RuleFor(vcp => vcp.Validator).NotNull().WithMessage("Validator cannot be null");
            RuleFor(vcp => vcp.Context).NotNull().WithMessage("Validation context cannot be null");
        }
    }
}

