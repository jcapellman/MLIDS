using System.Linq;

namespace MLIDS.lib.Extensions
{
    public static class CSV
    {
        public static string ToCSV<T>(this object obj) =>
            string.Join(',', typeof(T).GetProperties().Select(property => property.GetValue(obj)?.ToString()));

        public static T FromCSV<T>(this string line)
        {
            // Activator
            // Iterate through properties and set value
            // return T
        }
    }
}