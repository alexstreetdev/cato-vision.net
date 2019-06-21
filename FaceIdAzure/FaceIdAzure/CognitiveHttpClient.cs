
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceIdAzure
{
    /// <summary>
    /// Wrapper around HttpClient. Methods not static so that it is possible to mock.
    /// </summary>
    public class CognitiveHttpClient : ICognitiveHttpClient
    {

        private static readonly HttpClient _httpClient;

        private readonly string _endpoint;


        static CognitiveHttpClient()
        {
            _httpClient = new HttpClient();

        }

        public CognitiveHttpClient(AzureConfig azc)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", azc.ApiKey);
            _endpoint = azc.Endpoint;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            string fullUri = $"{_endpoint}{requestUri}";
            HttpResponseMessage hrm = await _httpClient.DeleteAsync(fullUri);
            return hrm;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            string fullUri = $"{_endpoint}{requestUri}";
            HttpResponseMessage hrm = await _httpClient.GetAsync(fullUri);
            return hrm;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent)
        {
            string fullUri = $"{_endpoint}{requestUri}";
            HttpResponseMessage hrm = await _httpClient.PostAsync(fullUri, httpContent);
            return hrm;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent httpContent)
        {
            string fullUri = $"{_endpoint}{requestUri}";
            HttpResponseMessage hrm = await _httpClient.PutAsync(fullUri, httpContent);
            return hrm;
        }

    }
}
