using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApiTemplate.SharedKernel.JsonConverters;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides JSON serialization configuration options for the Web API application.
    /// </summary>
    public static class JsonConfig
    {
        /// <summary>
        /// Configures JSON serialization options with custom converters.
        /// </summary>
        /// <param name="options">The <see cref="JsonOptions"/> to configure.</param>
        public static void ConfigureJsonOptions(this JsonOptions options)
        {
            // Add a JSON converter for string enums
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            // Add a custom JSON converter for DateTime objects
            options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());

            // Add a custom JSON converter for converting PermissionType
            options.JsonSerializerOptions.Converters.Add(new PermissionTypeConverter());
        }
    }
}
