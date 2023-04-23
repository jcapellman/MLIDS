using System;
using System.Linq;

namespace MLIDS.lib.Extensions
{
    public static class CSV
    {
        private const char SEPARATOR = ',';

        public static string ToCSV<T>(this object obj) =>
            string.Join(SEPARATOR, typeof(T).GetProperties().OrderBy(a => a.Name).Select(property => property.GetValue(obj)?.ToString()));

        public static T FromCSV<T>(this string line)
        {
            T obj = Activator.CreateInstance<T>();

            var properties = obj.GetType().GetProperties().OrderBy(a => a.Name);

            var lineProps = line.Split(SEPARATOR);

            var idx = 0;

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(Guid))
                {
                    prop.SetValue(obj, new Guid(lineProps[idx]));
                }
                else
                {
                    try
                    {
                        prop.SetValue(obj, Convert.ChangeType(lineProps[idx], prop.PropertyType), null);
                    } catch (FormatException fme)
                    {
                        Console.WriteLine(fme);
                    }
                }

                idx++;    
            }

            return obj;
        }
    }
}