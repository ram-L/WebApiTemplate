using Newtonsoft.Json;

namespace WebApiTemplate.SharedKernel.Helpers
{
    public static class Mappers
    {
        /// <summary>
        /// Maps properties from the source object to the destination object of a different type.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object whose properties will be mapped.</param>
        /// <param name="settings">Optional JSON serialization settings (default is null, uses default settings).</param>
        /// <returns>The destination object with mapped properties.</returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, JsonSerializerSettings settings = null)
        {
            // Use provided settings or create default settings if not provided
            if (settings == null)
                settings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            // Check if the source is null
            if (source == null) return default;

            try
            {
                string text = "";

                // Serialize the source object to JSON
                text = !source.GetType().Equals(typeof(string)) ? JsonConvert.SerializeObject(source) : source as string;

                // Deserialize the JSON to the destination type
                return JsonConvert.DeserializeObject<TDestination>(text, settings);
            }
            catch
            {
                // Handle any exceptions during mapping and return the default value for TDestination
                return default;
            }
        }
    }
}
