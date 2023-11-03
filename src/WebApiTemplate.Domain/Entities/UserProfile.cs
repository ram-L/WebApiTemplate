using WebApiTemplate.Domain.Bases;

namespace WebApiTemplate.Domain.Entities
{
    /// <summary>
    /// Represents a user profile entity.
    /// </summary>
    public partial class UserProfile : BaseEntity
    {
        /// <summary>
        /// Gets or sets the account ID associated with the user profile.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Gets or sets the associated account for the user profile.
        /// </summary>
        public virtual Account Account { get; protected set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash of the user.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the user.
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// Gets or sets the website URL associated with the user.
        /// </summary>
        public string Website { get; set; }
    }
}
