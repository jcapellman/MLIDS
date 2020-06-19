using System.Collections.Generic;
using System.IO;

namespace MLIDS.Scripter.lib
{
    public class VectorParser
    {
        public List<BaseVector> ParseScript(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"VectorParser::ParseScript - File not found {fileName}");
            }



            return new List<BaseVector>();
        }
    }
}