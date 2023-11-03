using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Interfaces
{
    public interface IEntityTracking
    {
        TrackingState EntityState { get; }
    }
}