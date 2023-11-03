namespace WebApiTemplate.Application.Extensions
{
    public static class EnumerableExtensions
    {
        public static Dictionary<TKey, TElement> ToDictionarySafe<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            var resultDictionary = new Dictionary<TKey, TElement>();

            foreach (var item in source)
            {
                try
                {
                    var key = keySelector.Invoke(item);

                    // Validate the key; it cannot be null or an empty string
                    var keyString = typeof(TKey).IsEnum ? $"{key}" : key as string;
                    if (key == null || string.IsNullOrEmpty(keyString))
                        continue;

                    // Add or update the dictionary entry
                    resultDictionary[key] = elementSelector(item);
                }
                catch (Exception ex)
                {
                    // Handle exceptions if necessary
                }
            }

            return resultDictionary;
        }
    }
}
