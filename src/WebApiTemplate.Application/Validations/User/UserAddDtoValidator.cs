using FluentValidation;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.Application.Validations.Common;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.User
{
    public class UserAddDtoValidator : ValidatorBase<UserAddDto>
    {
        public UserAddDtoValidator(ICustomLogger logger) : base(logger)
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8)
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(x => $"{nameof(x.Password)} must be at least 8 characters long.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage(x => $"{nameof(x.ConfirmPassword)} must match the {nameof(x.Password)}.");
        }
    }
}
