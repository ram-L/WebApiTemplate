using FluentValidation;
using WebApiTemplate.Application.Validations.Common;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.Application.Validations.User
{
    public class ClientValidator : ValidatorBase<Account>
    {
        public ClientValidator(ICustomLogger logger) : base(logger)
        {
           
        }
    }
}
