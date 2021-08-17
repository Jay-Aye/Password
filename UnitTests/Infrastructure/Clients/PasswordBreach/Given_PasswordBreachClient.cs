using Core.Interfaces;
using FluentAssertions;
using Infrastructure.Clients.PasswordBreach;
using Moq;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace UnitTests.Infrastructure.Clients.PasswordBreach
{
    public class Given_PasswordBreachClient
    {
        private readonly Mock<IHttpService> _mockHttpService;

        private readonly IPasswordBreachClient _passwordBreachClient;

        public Given_PasswordBreachClient()
        {
            _mockHttpService = new Mock<IHttpService>();
            _passwordBreachClient = new PasswordBreachClient(_mockHttpService.Object);
        }

        [Fact]
        public async void When_Hash_Then_ProcessDetailsToDownstreamServices()
        {
            // Arrange
            var mockPassword = "password";
            var expectedUrl = "range/5BAA6";
            var mockContentFileContent = File.ReadAllText($"{Directory.GetCurrentDirectory()}/Responses/PasswordBreachClient/PwnResponse.txt");
            var mockContentString = mockContentFileContent.Replace("\\r", "\r");
            mockContentString = mockContentString.Replace("\\n", "\n");
            var mockContentResponse = Encoding.UTF8.GetBytes(mockContentString);

            _mockHttpService.Setup(x => x.SendRequest(It.IsAny<string>(), HttpMethod.Get))
                .ReturnsAsync(new HttpResponseMessage { Content = new ByteArrayContent(mockContentResponse)});
            //Act
            var result = await _passwordBreachClient.GetPasswordBreachCount(mockPassword);

            //Assert
            result.Should().Be(3861493);
            _mockHttpService.Verify(x => x.SendRequest(expectedUrl, HttpMethod.Get), Times.Once);
            _mockHttpService.VerifyNoOtherCalls();
        }
    }
}
