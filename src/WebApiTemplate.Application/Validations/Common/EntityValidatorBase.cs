using FluentValidation;
using WebApiTemplate.Domain.Interfaces;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.Common
{
    public abstract class EntityValidatorBase<T> : AbstractValidator<T>
        where T : class, IEntity, IEntityTracking
    {
        private readonly ICustomLogger _logger;

        protected EntityValidatorBase(ICustomLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected static bool IsForCreate(TrackingState state)
            => state != TrackingState.New;

        protected static bool IsForUpdate(TrackingState state)
            => !new[] { TrackingState.New, TrackingState.Added, TrackingState.HardDeleted }.Contains(state);
    }
}
