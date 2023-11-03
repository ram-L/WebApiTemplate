using System.ComponentModel.DataAnnotations.Schema;
using WebApiTemplate.Domain.Interfaces;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Domain.Bases
{
    public abstract partial class BaseEntity : IEntityTracking
    {
        [NotMapped]
        public TrackingState EntityState { get; protected set; } = TrackingState.New;

        /// <summary>
        /// Updates the entity's tracking state.
        /// </summary>
        /// <param name="newState">The new tracking state.</param>
        protected void UpdateState(TrackingState newState)
        {
            // Define invalid state transitions
            var invalidTransitions = new Dictionary<TrackingState, TrackingState[]>
            {
                { TrackingState.New, new[] { TrackingState.New, TrackingState.Added, TrackingState.Modified, TrackingState.Unchanged, TrackingState.SoftDeleted, TrackingState.HardDeleted } },
                { TrackingState.Added, new[] { TrackingState.Added } },
                { TrackingState.Modified, new[] { TrackingState.Modified, TrackingState.Added } },
                { TrackingState.SoftDeleted, new[] { TrackingState.SoftDeleted } },
                { TrackingState.HardDeleted, new[] { TrackingState.HardDeleted } },
                { TrackingState.Unchanged, Array.Empty<TrackingState>() }
            };

            if (invalidTransitions[EntityState].Contains(newState))
                return;

            EntityState = newState;
        }

        /// <summary>
        /// Resets the entity's tracking state to Unchanged.
        /// </summary>
        public void ResetState() => UpdateState(TrackingState.Unchanged);

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        /// <param name="hardDelete">Indicates whether to perform a hard delete.</param>
        public void Delete(bool hardDelete = false) => UpdateState(hardDelete ? TrackingState.HardDeleted : TrackingState.SoftDeleted);
    }

}
