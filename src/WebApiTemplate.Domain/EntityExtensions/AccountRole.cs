namespace WebApiTemplate.Domain.Entities
{
    public partial class AccountRole
    {
        /// <summary>
        /// Creates an account role relationship with the specified account and role IDs.
        /// </summary>
        /// <param name="accountId">The ID of the account to associate with the role.</param>
        /// <param name="roleId">The ID of the role to associate with the account.</param>
        /// <returns>The newly created account role relationship.</returns>
        public AccountRole Create(int accountId, int roleId)
        {
            AccountId = accountId;
            RoleId = roleId;

            return this;
        }
    }
}
