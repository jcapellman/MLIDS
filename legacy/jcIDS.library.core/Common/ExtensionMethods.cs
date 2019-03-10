using System.Linq;

using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.Common
{
    public static class ExtensionMethods
    {
        public static string ToCSV(this BaseObject baseObject)
        {
            var properties = baseObject.GetType().GetProperties();

            var values = properties.Select(property => property.GetValue(baseObject).ToString()).ToList();

            return string.Join(",", values);
        }
    }
}