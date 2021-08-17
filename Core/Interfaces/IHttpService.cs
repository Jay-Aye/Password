using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendRequest(string url, HttpMethod method);
    }
}
