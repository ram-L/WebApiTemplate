namespace WebApiTemplate.SharedKernel.Extensions
{
    /// <summary>
    /// Provides extension methods for converting objects of known types to strings.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts an object to a string or returns an empty string if the object is null.
        /// </summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <returns>
        /// The string representation of the object, or an empty string if the object is null.
        /// </returns>
        public static string ToStringOrEmpty(this object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Converts an object to a string or returns a default string value if the object is null.
        /// </summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <param name="defaultVal">The default string value to return if the object is null.</param>
        /// <returns>
        /// The string representation of the object, or the specified default value if the object is null.
        /// </returns>
        public static string ToStringOrDefault(this object value, string defaultVal)
        {
            return value?.ToString() ?? defaultVal;
        }
    }
}
