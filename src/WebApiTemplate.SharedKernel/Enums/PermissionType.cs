namespace WebApiTemplate.SharedKernel.Enums
{
    [Flags]
    public enum PermissionType
    {
        /// <summary>
        /// No permission is granted.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Permission to retrieve data from a resource.
        /// </summary>
        Read = 0x1,

        /// <summary>
        /// Permission to create/insert data into a resource.
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Permission to change/update data in a resource.
        /// </summary>
        Update = 0x4,

        /// <summary>
        /// Permission to delete data from a resource.
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// Full access permission, granting the ability to Read, Create, Update, and Delete data from a resource.
        /// </summary>
        Full = Read | Create | Update | Delete
    }
}
