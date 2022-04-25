using System;
using System.Linq;

using Microsoft.ML.Data;

namespace MLIDS.lib.Extensions
{
    public static class ExtensionMethods
    {
        public static string[] ToPropertyList(this Type objType, string labelName) => 
            objType.GetProperties().Where(a => a.Name != labelName && 
                a.CustomAttributes.All(b => b.AttributeType != typeof(NoColumnAttribute)))
                .Select(a => a.Name).ToArray();
    }
}