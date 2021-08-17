using Core.Interfaces;
using Core.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Clients.PasswordBreach
{
    public class PasswordBreachClient : IPasswordBreachClient
    {
        private readonly IHttpService _httpService;

        public PasswordBreachClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> GetPasswordBreachCount(string password)
        {
            var hashedPassword = password.Hash();
            var requestHash = hashedPassword.Substring(0, 5);
            var matchHash = hashedPassword.Substring(5);
            var url = $"range/{requestHash}";
            
            var response = await _httpService.SendRequest(url, HttpMethod.Get);
            
            var responseData = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var responseString = Encoding.UTF8.GetString(responseData, 0, responseData.Length);
            var result = responseString.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var breachCountMatch = result.FirstOrDefault(x => x.StartsWith(requestHash));

            if (breachCountMatch == null)
            {
                return 0;
            }

            var breachCountString = breachCountMatch.Split(':').Last();

            return int.Parse(breachCountString);
        }
    }
}
