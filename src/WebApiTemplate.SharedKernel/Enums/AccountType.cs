namespace WebApiTemplate.SharedKernel.Enums
{
    public enum AccountType
    {
        /// <summary>
        /// Represents a standard user with full interactive access to the application.
        /// </summary>
        User,

        /// <summary>
        /// Represents non-human entities, such as hardware, other APIs, or console applications,
        /// that do not require user interactions.
        /// </summary>
        Client,

        /// <summary>
        /// Represents an external authentication provider used for user login.
        /// </summary>
        ExternalUser
    }
}
