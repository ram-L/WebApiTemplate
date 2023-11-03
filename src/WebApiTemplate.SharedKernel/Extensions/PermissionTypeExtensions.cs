using FluentValidation;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.SharedKernel.Extensions
{
    /// <summary>
    /// Provides extension methods for converting between PermissionType enum and strings.
    /// </summary>
    public static class PermissionTypeExtensions
    {
        /// <summary>
        /// Converts a PermissionType enum to a string representation.
        /// </summary>
        /// <param name="permissions">The PermissionType enum to convert.</param>
        /// <param name="separator">The separator to use between multiple PermissionType values.</param>
        /// <returns>A string representation of the PermissionType enum.</returns>
        public static string ConvertToString(this PermissionType permissions, string separator = "|")
        {
            var validPermissions = Enum.GetValues(typeof(PermissionType))
                .OfType<PermissionType>()
                .Where(p => p != PermissionType.None && p != PermissionType.Full && permissions.HasFlag(p))
                .Select(p => p.ToString());

            return string.Join(separator, validPermissions);
        }

        /// <summary>
        /// Parses a string representation of PermissionType values and returns the corresponding PermissionType enum.
        /// </summary>
        /// <param name="value">The string containing PermissionType values.</param>
        /// <param name="separator">The separator used in the string.</param>
        /// <returns>A PermissionType enum parsed from the input string.</returns>
        public static PermissionType ParseToPermissionType(this string value, string separator = "|")
        {
            if (string.IsNullOrWhiteSpace(value))
                return PermissionType.None;

            var permissionStrings = value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            PermissionType parsedPermissions = PermissionType.None;

            foreach (var permissionString in permissionStrings)
            {
                if (Enum.TryParse(permissionString, out PermissionType parsedPermission))
                {
                    parsedPermissions |= parsedPermission;
                }
            }

            return parsedPermissions;
        }
    }
}

