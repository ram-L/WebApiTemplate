using WebApiTemplate.Domain.Bases;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    /// <summary>
    /// Represents a client profile entity.
    /// </summary>
    public partial class ClientProfile : BaseEntity
    {
        /// <summary>
        /// Gets or sets the account ID associated with the client profile.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Gets or sets the associated account for the client profile.
        /// </summary>
        public virtual Account Account { get; protected set; }

        /// <summary>
        /// Gets or sets the type of the client.
        /// </summary>
        public ClientType ClientType { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the description of the client.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unique key associated with the client.
        /// </summary>
        public string ClientKey { get; set; }
    }
}
