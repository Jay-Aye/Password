using Core.Services.Http;
using FluentAssertions;
using Infrastructure.Clients.PasswordBreach;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{

    public class Given_PasswordBreachClient
    {
        private readonly PasswordBreachClient _passwordBreachClient;

        public Given_PasswordBreachClient()
        {
            var configuration = Setup.GivenConfiguration();
            var httpClient = new HttpClient();
            var httpService = new HttpService(httpClient, configuration);
            _passwordBreachClient = new PasswordBreachClient(httpService);
        }

        [Fact]
        public async Task When_GetPasswordBreachCount_Then_GetResultsFromThirdPartyAndMatchWithOriginalHash()
        {
            // Arrange
            var password = "password";

            // Act
            var result = await _passwordBreachClient.GetPasswordBreachCount(password);

            // Assert
            result.Should().BeOfType(typeof(int));
            result.Should().BeGreaterThan(100000);
        }
    }
}
