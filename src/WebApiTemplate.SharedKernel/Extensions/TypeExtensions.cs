using System.Collections;
using System.Reflection;

namespace WebApiTemplate.SharedKernel.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the empty or default value for a given type.
        /// </summary>
        /// <param name="type">The type for which to obtain the default value.</param>
        /// <returns>The default value for the specified type.</returns>
        public static object GetEmptyOrDefaultValue(this Type type)
        {
            // Handle value types
            if (type.IsValueType)
            {
                // Handle nullable value types
                if (Nullable.GetUnderlyingType(type) != null)
                {
                    return null;
                }
                return Activator.CreateInstance(type);
            }

            // Handle strings
            if (type == typeof(string))
            {
                return string.Empty;
            }

            // Handle collections
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (type.IsArray)
                {
                    return Array.CreateInstance(type.GetElementType(), 0);
                }
                else if (type.IsGenericType)
                {
                    Type genericType = type.GetGenericTypeDefinition();
                    Type genericArgument = type.GetGenericArguments()[0];
                    Type constructedListType = genericType.MakeGenericType(genericArgument);
                    return Activator.CreateInstance(constructedListType);
                }
                else
                {
                    return Activator.CreateInstance(typeof(ArrayList));
                }
            }

            // Handle classes with a parameterless constructor
            if (type.IsClass)
            {
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    return Activator.CreateInstance(type);
                }
            }

            // If none of the above conditions are met, return null
            return null;
        }

        /// <summary>
        /// Converts an object to the specified type if possible, or returns the default value for the type.
        /// </summary>
        /// <typeparam name="T">The target type to which the object should be converted.</typeparam>
        /// <param name="input">The object to convert.</param>
        /// <returns>The converted object of type T, or the default value of type T if the conversion is not possible.</returns>
        public static T ConvertTo<T>(this object input)
        {
            try
            {
                if (input == null)
                {
                    return default(T);
                }

                // If the input is already of the desired type, return it directly
                if (input is T t)
                {
                    return t;
                }

                // Attempt to convert the input to the desired type
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (InvalidCastException)
            {
                // Handle the exception if the conversion is not possible
                return default(T);
            }
        }
    }
}
