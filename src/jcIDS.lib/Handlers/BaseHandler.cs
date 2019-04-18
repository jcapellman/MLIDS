using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using jcIDS.lib.CommonObjects;

using Newtonsoft.Json;

namespace jcIDS.lib.Handlers
{
    public abstract class BaseHandler : IDisposable
    {
        private readonly string _hostName;

        protected abstract string EndPoint { get; }

        /// <summary>
        /// Initializes the BaseHandler with the hostname
        /// </summary>
        /// <param name="hostname"></param>
        protected BaseHandler(string hostname)
        {
            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException(nameof(hostname));
            }

            _hostName = hostname;
        }

        /// <summary>
        /// Builds the full URL based on the endpoint and hostname
        /// </summary>
        /// <param name="url">The endpoint</param>
        /// <returns>Full URL to be used to connect to the server</returns>
        private string BuildUrl(string url) => $"{_hostName}{Common.Constants.BASE_URL}{url}";

        /// <summary>
        /// Asynchronous Post helper method, accepts a type of T and returns a type of TK
        /// </summary>
        /// <typeparam name="T">Request Type</typeparam>
        /// <typeparam name="TK">Response Type</typeparam>
        /// <param name="url">Endpoint to hit</param>
        /// <param name="dataToPost">Type of T object to Post</param>
        /// <returns>Type of TK or Exception if in error</returns>
        public async Task<ReturnSet<TK>> PostAsync<T, TK>(string url, T dataToPost)
        {
            var responseText = string.Empty;
            var requestUrl = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(dataToPost), Encoding.UTF8, "application/json");

                    requestUrl = BuildUrl(url);

                    var result = await httpClient.PostAsync(requestUrl, content);

                    responseText = await result.Content.ReadAsStringAsync();

                    return new ReturnSet<TK>(JsonConvert.DeserializeObject<TK>(responseText));
                }
            }
            catch (Exception ex)
            {
                return new ReturnSet<TK>(ex, $"URL: {requestUrl} | Response: {responseText}");
            }
        }

        public void Dispose()
        {
        }
    }
}