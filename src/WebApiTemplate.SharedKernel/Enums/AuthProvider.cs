namespace WebApiTemplate.SharedKernel.Enums
{
    public enum AuthProvider
    {
        /// <summary>
        /// Represents authentication provided by the application itself.
        /// </summary>
        Own = 0,

        /// <summary>
        /// Represents authentication using Google accounts.
        /// </summary>
        Google,

        /// <summary>
        /// Represents authentication using Facebook accounts.
        /// </summary>
        Facebook,

        /// <summary>
        /// Represents authentication using Twitter accounts.
        /// </summary>
        Twitter,

        /// <summary>
        /// Represents authentication using GitHub accounts.
        /// </summary>
        GitHub,

        /// <summary>
        /// Represents authentication using Azure Active Directory (AzureAD).
        /// </summary>
        AzureAD,

        /// <summary>
        /// Represents authentication using Windows Active Directory (WinAD).
        /// </summary>
        WinAD
    }
}
