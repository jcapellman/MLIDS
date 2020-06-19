using System.IO;
using System.Net.Http;

using MLIDS.Scripter.lib.Enums;

namespace MLIDS.Scripter.lib.Vectors
{
    public class WebDownloadVector : BaseVector
    {
        public override ScriptVectors ScriptType => ScriptVectors.WEB_DOWNLOAD;

        public override int NumRequiredArguments => 2;

        public override async void Execute()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetByteArrayAsync(Arguments[0]);

                await File.WriteAllBytesAsync(Arguments[1], result);
            }
        }
    }
}