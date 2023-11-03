using FluentValidation;
using WebApiTemplate.Domain.Interfaces;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.Common
{
    public abstract class ValidatorBase<T> : AbstractValidator<T> where T : class
    {
        protected readonly ICustomLogger _logger;

        protected ValidatorBase(ICustomLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}