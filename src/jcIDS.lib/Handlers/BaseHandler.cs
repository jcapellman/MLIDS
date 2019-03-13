using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcIDS.lib.Handlers
{
    public class BaseHandler
    {
        public async Task<TK> PostAsync<T, TK>(string url, T dataToPost)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(dataToPost);

                    var result = await httpClient.PostAsync($"{Common.Constants.BASE_URL}{url}",
                        new StringContent(data, Encoding.UTF8, "application/json"));

                    var response = await result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TK>(response);
                }
            }
            catch (Exception ex)
            {
                // TODO: Log exception

                return default;
            }
        }
    }
}