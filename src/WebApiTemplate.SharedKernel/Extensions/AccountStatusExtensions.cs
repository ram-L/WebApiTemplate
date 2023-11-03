using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.SharedKernel.Extensions
{
    public static class AccountStatusExtensions
    {
        public static string GetAccountMessage(this AccountStatus accountStatus)
        {
            //switch(accountStatus)
            //{
            //    case AccountStatus
            //}

            return $"Your account has been {accountStatus}. Please contact the administrator.";
        }
    }
}

