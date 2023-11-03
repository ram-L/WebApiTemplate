using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.SharedKernel.Extensions
{
    /// <summary>
    /// Provides extension methods for parsing strings into various common data types.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to an <see cref="AccountType"/> enum value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed <see cref="AccountType"/> enum value, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static AccountType? ToAccountType(this string value, AccountType? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (Enum.TryParse(value, out AccountType outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to an integer.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed integer, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static int? ToInt(this string value, int? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (int.TryParse(value, out int outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a double.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed double, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static double? ToDouble(this string value, double? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (double.TryParse(value, out double outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed <see cref="DateTime"/> object, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static DateTime? ToDateTime(this string value, DateTime? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (DateTime.TryParse(value, out DateTime outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed boolean value, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static bool? ToBool(this string value, bool? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (bool.TryParse(value, out bool outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a decimal.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed decimal, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static decimal? ToDecimal(this string value, decimal? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (decimal.TryParse(value, out decimal outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a long integer.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed long integer, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static long? ToLong(this string value, long? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (long.TryParse(value, out long outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a <see cref="Guid"/> object.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed <see cref="Guid"/> object, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static Guid? ToGuid(this string value, Guid? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (Guid.TryParse(value, out Guid outVal))
                return outVal;

            return defaultVal;
        }

        /// <summary>
        /// Converts a string to a <see cref="TimeSpan"/> object.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultVal">The default value to return if the conversion fails or the string is empty or null.</param>
        /// <returns>The parsed <see cref="TimeSpan"/> object, or <paramref name="defaultVal"/> if the conversion fails or the string is empty or null.</returns>
        public static TimeSpan? ToTimeSpan(this string value, TimeSpan? defaultVal = null)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;

            if (TimeSpan.TryParse(value, out TimeSpan outVal))
                return outVal;

            return defaultVal;
        }
    }
}
