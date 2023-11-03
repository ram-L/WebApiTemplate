using FluentValidation;
using WebApiTemplate.Application.Validations.Common;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.User
{
    public class UserValidator : ValidatorBase<Account>
    {
        public UserValidator(ICustomLogger logger) : base(logger)
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0)
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage("Owner is required.");

            RuleFor(x => x.UserProfile)
                .NotNull()
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage("User has not been created.");

            RuleFor(x => x.UserProfile.PasswordHash)
                .NotEmpty()
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage("Password is required.");

            RuleFor(x => x.UserProfile.Firstname)
                .NotEmpty()
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage("First name is required.");

            RuleFor(x => x.UserProfile.Surname)
                .NotEmpty()
                .WithErrorCode($"{ErrorCode.ValidationError}")
                .WithMessage("Surname is required.");

            RuleFor(x => x.UserProfile.Email)
                .NotEmpty().WithErrorCode($"{ErrorCode.ValidationError}").WithMessage("Email is required.")
                .EmailAddress().WithErrorCode($"{ErrorCode.ValidationError}").WithMessage("Invalid email address.");
        }
    }
}
