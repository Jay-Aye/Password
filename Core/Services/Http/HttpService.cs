using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public HttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["PwnedEndpointUrl"];
        }

        public async Task<HttpResponseMessage> SendRequest(string url, HttpMethod method)
        {
            return await Execute(url, method);
        }

        private async Task<HttpResponseMessage> Execute(string url, HttpMethod method)
        {
            var uri = new Uri($"{_baseUrl.TrimEnd('/')}/{url}", UriKind.RelativeOrAbsolute);

            using (var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            })
            {
                HttpResponseMessage response;

                request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                return response;
            }
        }

    }
}
