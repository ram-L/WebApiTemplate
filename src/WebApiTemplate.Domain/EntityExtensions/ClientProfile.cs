using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Entities
{
    public partial class ClientProfile
    {
        /// <summary>
        /// Creates a client profile with the specified details.
        /// </summary>
        /// <param name="clientType">The type of the client.</param>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="description">The description of the client.</param>
        /// <param name="clientKey">The unique key associated with the client.</param>
        /// <returns>The newly created client profile with the specified details.</returns>
        public ClientProfile Create(ClientType clientType, string clientName, string description, string clientKey)
        {
            ClientType = clientType;
            ClientName = clientName;
            Description = description;
            ClientKey = clientKey;

            return this;
        }

        /// <summary>
        /// Updates the client name and description.
        /// </summary>
        /// <param name="clientName">The updated client name.</param>
        /// <param name="description">The updated description.</param>
        public void Update(string clientName, string description)
        {
            ClientName = clientName;
            Description = description;
        }
    }
}
