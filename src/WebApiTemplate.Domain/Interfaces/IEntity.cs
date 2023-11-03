namespace WebApiTemplate.Domain.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the primary key uniquely identifying the entity.
        /// </summary>
        int Id { get; }
    }
}