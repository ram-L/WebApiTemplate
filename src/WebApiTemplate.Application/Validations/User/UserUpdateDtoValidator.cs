using FluentValidation;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.Application.Validations.Common;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.User
{
    public class UserUpdateDtoValidator : ValidatorBase<UserUpdateDto>
    {
        public UserUpdateDtoValidator(ICustomLogger logger) : base(logger)
        {
            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithErrorCode($"{ErrorCode.ValidationError}")
               .WithMessage("User Id is required.");

            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithErrorCode($"{ErrorCode.ValidationError}")
               .WithMessage("Email is required.");
        }
    }
}
