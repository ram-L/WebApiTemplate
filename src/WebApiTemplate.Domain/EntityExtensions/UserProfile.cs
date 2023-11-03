namespace WebApiTemplate.Domain.Entities
{
    public partial class UserProfile
    {
        /// <summary>
        /// Creates a user profile with the specified details.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="passwordHash">The password hash of the user.</param>
        /// <param name="firstname">The first name of the user.</param>
        /// <param name="surname">The surname of the user.</param>
        /// <param name="email">The email address associated with the user.</param>
        /// <param name="contactNo">The contact number of the user (optional).</param>
        /// <param name="website">The website URL associated with the user (optional).</param>
        /// <returns>The newly created user profile with the specified details.</returns>
        public UserProfile Create(string username, string passwordHash, string firstname, string surname, string email, string contactNo = null, string website = null)
        {
            Username = username;
            PasswordHash = passwordHash;
            Firstname = firstname;
            Surname = surname;
            Email = email;
            ContactNo = contactNo;
            Website = website;

            return this;
        }

        /// <summary>
        /// Updates the user profile with the specified details.
        /// </summary>
        /// <param name="firstname">The updated first name of the user.</param>
        /// <param name="surname">The updated surname of the user.</param>
        /// <param name="email">The updated email address associated with the user.</param>
        /// <param name="contactNo">The updated contact number of the user (optional).</param>
        /// <param name="website">The updated website URL associated with the user (optional).</param>
        public void Update(string firstname, string surname, string email, string contactNo = null, string website = null)
        {
            Firstname = firstname;
            Surname = surname;
            Email = email;
            ContactNo = contactNo;
            Website = website;
        }

        /// <summary>
        /// Adds or updates the password hash for the user.
        /// </summary>
        /// <param name="passwordHash">The new password hash to set.</param>
        public void AddPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
