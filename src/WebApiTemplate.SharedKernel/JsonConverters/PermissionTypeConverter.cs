using System.Text.Json;
using System.Text.Json.Serialization;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Extensions;

namespace WebApiTemplate.SharedKernel.JsonConverters
{
    /// <summary>
    /// Provides a JSON converter for converting between PermissionType enum and JSON strings.
    /// </summary>
    public class PermissionTypeConverter : JsonConverter<PermissionType>
    {
        /// <summary>
        /// Reads a PermissionType enum from a JSON string representation.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The target type to convert to.</param>
        /// <param name="options">The serializer options.</param>
        /// <returns>The parsed PermissionType enum.</returns>
        public override PermissionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringVal = reader.GetString();
                return stringVal.ParseToPermissionType();
            }

            // Throw a JsonException if parsing fails.
            throw new JsonException("Failed to parse PermissionType.");
        }

        /// <summary>
        /// Writes a PermissionType enum as a JSON string representation.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The PermissionType enum to write.</param>
        /// <param name="options">The serializer options.</param>
        public override void Write(Utf8JsonWriter writer, PermissionType value, JsonSerializerOptions options)
        {
            // Format the PermissionType enum as a string and write it to the JSON writer.
            writer.WriteStringValue(value.ConvertToString());
        }
    }
}
