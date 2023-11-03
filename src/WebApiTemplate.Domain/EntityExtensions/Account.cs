using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class Account
    {
        /// <summary>
        /// Creates an account with the specified account type and sets the associated profile.
        /// </summary>
        /// <param name="currentId">The ID of the current user creating the account.</param>
        /// <param name="accountType">The type of the account to create.</param>
        /// <returns>The newly created account with the specified account type and associated profile.</returns>
        public Account Create(int currentId, AccountType accountType)
        {
            AccountType = accountType;

            switch (accountType)
            {
                default:
                case AccountType.User: UserProfile = new UserProfile(); break;
                case AccountType.Client: ClientProfile = new ClientProfile(); break;
                case AccountType.ExternalUser: ExternalUserProfile = new ExternalUserProfile(); break;
            }

            SetCreateAudit(currentId);

            return this;
        }

        /// <summary>
        /// Updates the status of the account.
        /// </summary>
        /// <param name="currentId">The ID of the current user updating the account.</param>
        /// <param name="status">The new status to set for the account.</param>
        public void Update(int currentId, AccountStatus status)
        {
            Status = status;
            SetUpdateAudit(currentId);
        }
    }
}
