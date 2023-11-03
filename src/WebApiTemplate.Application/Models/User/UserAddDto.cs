namespace WebApiTemplate.Application.Models.User
{
    /// <summary>
    /// Data transfer object for adding a new user.
    /// </summary>
    public class UserAddDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the user.
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the website URL of the user.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner associated with the user.
        /// </summary>
        public int OwnerId { get; set; }
    }
}
