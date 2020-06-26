using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MLIDS.Scripter.lib
{
    public class VectorParser
    {
        public static string ToJson(IEnumerable<BaseVector> scriptEntries)
        {
            return string.Empty;
        }

        public async Task<List<BaseVector>> ParseScriptAsync(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"VectorParser::ParseScript - File not found {fileName}");
            }

            using (var fs = File.OpenRead(fileName))
            {
                var scriptVectors = await JsonSerializer.DeserializeAsync<List<BaseVector>>(fs);

                return scriptVectors;
            }
        }
    }
}