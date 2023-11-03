using WebApiTemplate.Domain.Interfaces;

namespace WebApiTemplate.Domain.Bases
{
    public abstract partial class BaseEntity : IEntity
    {        
        public int Id { get; protected set; }
    }
}
