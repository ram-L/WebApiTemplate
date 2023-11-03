namespace WebApiTemplate.SharedKernel.Enums
{
    public enum TrackingState
    {
        /// <summary>
        /// Indicates a new entity that has not been saved to the database yet.
        /// </summary>
        New = -1,

        /// <summary>
        /// Indicates an entity that has not been modified since it was retrieved from the database.
        /// </summary>
        Unchanged = 0,

        /// <summary>
        /// Indicates an entity that has been added and is pending insertion into the database.
        /// </summary>
        Added = 1,

        /// <summary>
        /// Indicates an entity that has been modified and needs to be updated in the database.
        /// </summary>
        Modified = 2,

        /// <summary>
        /// Indicates an entity that has been marked as soft deleted (logical delete).
        /// </summary>
        SoftDeleted = 3,

        /// <summary>
        /// Indicates an entity that has been marked as hard deleted (physical delete).
        /// </summary>
        HardDeleted = 4
    }
}
