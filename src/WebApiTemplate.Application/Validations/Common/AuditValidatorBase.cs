using FluentValidation;
using WebApiTemplate.Domain.Interfaces;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.Common
{
    public abstract class AuditValidatorBase<T> : EntityValidatorBase<T>
        where T : class, IAuditEntity, IEntityTracking
    {
        private const string CreatedDateRequired = "Created Date is required.";
        private const string CreatedByRequired = "Created By is required.";
        private const string ModifiedDateRequired = "Modified Date is required.";
        private const string ModifiedByRequired = "Modified By is required.";

        protected AuditValidatorBase(ICustomLogger logger) : base(logger)
        {
            RuleFor(x => x.CreatedDate)
                .NotNull()
                .When(x => IsForCreate(x.EntityState))
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(CreatedDateRequired);

            RuleFor(x => x.CreatedById)
                .GreaterThan(0)
                .When(x => IsForCreate(x.EntityState))
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(CreatedByRequired);

            RuleFor(x => x.ModifiedDate)
                .NotNull()
                .When(x => IsForUpdate(x.EntityState))
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(ModifiedDateRequired);

            RuleFor(x => x.ModifiedById)
                .GreaterThan(0)
                .When(x => IsForUpdate(x.EntityState))
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(ModifiedByRequired);
        }
    }
}
