using FluentValidation;

namespace WebApiTemplate.SharedKernel.Models
{
    /// <summary>
    /// Represents a pair of FluentValidation validator and validation context.
    /// </summary>
    public class ValidatorContextPair
    {
        /// <summary>
        /// Gets the FluentValidation validator associated with the pair.
        /// </summary>
        public IValidator Validator { get; }

        /// <summary>
        /// Gets the validation context associated with the pair.
        /// </summary>
        public IValidationContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorContextPair"/> class with a validator and validation context.
        /// </summary>
        /// <param name="validator">The FluentValidation validator to associate with the pair.</param>
        /// <param name="context">The validation context to associate with the pair.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="validator"/> or <paramref name="context"/> is null.</exception>
        public ValidatorContextPair(IValidator validator, IValidationContext context)
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
