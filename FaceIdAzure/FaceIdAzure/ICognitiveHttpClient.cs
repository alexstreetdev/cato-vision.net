
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceIdAzure
{
    public interface ICognitiveHttpClient
    {

        Task<HttpResponseMessage> DeleteAsync(string requestUri);

        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent);

        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent httpContent);

    }
}
