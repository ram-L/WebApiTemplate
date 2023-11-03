namespace WebApiTemplate.SharedKernel.Enums
{
    /// <summary>
    /// Represents the status of an account in the application.
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// The account is active and fully functional.
        /// </summary>
        Active,

        /// <summary>
        /// The account has been suspended, restricting its functionality temporarily.
        /// </summary>
        Suspended,

        /// <summary>
        /// The account is pending activation or approval.
        /// </summary>
        Pending,

        /// <summary>
        /// The account has been banned and is prohibited from accessing the application.
        /// </summary>
        Banned,

        /// <summary>
        /// The account has expired, and access is no longer allowed.
        /// </summary>
        Expired,

        /// <summary>
        /// The account has been locked, preventing access until further action is taken.
        /// </summary>
        Locked,

        /// <summary>
        /// The account has been deleted and is no longer available in the system.
        /// </summary>
        Deleted
    }
}
