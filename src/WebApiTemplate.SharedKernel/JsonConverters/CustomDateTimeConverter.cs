using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApiTemplate.SharedKernel.JsonConverters
{
    /// <summary>
    /// A custom JSON converter for serializing and deserializing DateTime values in a specific format.
    /// </summary>
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// Deserializes a DateTime value from JSON.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The JSON serializer options.</param>
        /// <returns>The deserialized DateTime value.</returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                // Attempt to parse the DateTime value in a specific format.
                if (DateTime.TryParseExact(reader.GetString(), "yyyy-MM-ddTHH:mm:ssZ", null, DateTimeStyles.RoundtripKind, out DateTime dateTime))
                {
                    return dateTime;
                }
            }

            // Throw a JsonException if parsing fails.
            throw new JsonException("Failed to parse DateTime.");
        }

        /// <summary>
        /// Serializes a DateTime value to JSON in a specific format.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The DateTime value to serialize.</param>
        /// <param name="options">The JSON serializer options.</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Format the DateTime value as a string in the desired format and write it to the JSON writer.
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }
    }
}
