using Core.Interfaces;
using Core.Services.PasswordService;
using FluentAssertions;
using Models.Password;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Core.Services
{
    public class Given_PasswordService
    {
        private readonly Mock<IPasswordBreachClient> _mockPasswordBreachClient;
        private readonly Mock<IPasswordStrengthService> _mockPasswordStrengthService;

        private readonly IPasswordService _passwordService;

        public Given_PasswordService()
        {
            _mockPasswordBreachClient = new Mock<IPasswordBreachClient>();
            _mockPasswordStrengthService = new Mock<IPasswordStrengthService>();

            _passwordService = new PasswordService(_mockPasswordBreachClient.Object, _mockPasswordStrengthService.Object);
        }

        [Fact]
        public async void When_GetPasswordDetails_Then_ProcessDetailsToDownstreamServices()
        {
            // Arrange
            var mockPassword = "P@ssword";

            _mockPasswordBreachClient.Setup(x => x.GetPasswordBreachCount(It.IsAny<string>()))
                .Returns(Task.FromResult(10));

            _mockPasswordStrengthService.Setup(x => x.GetPasswordStrength(It.IsAny<string>()))
                .Returns(Task.FromResult(PasswordStrength.VeryStrong));

            //Act
            var result = await _passwordService.GetPasswordDetails(mockPassword);

            //Assert
            _mockPasswordBreachClient.Verify(x => x.GetPasswordBreachCount(mockPassword), Times.Once);
            _mockPasswordBreachClient.VerifyNoOtherCalls();

            _mockPasswordStrengthService.Verify(x => x.GetPasswordStrength(mockPassword), Times.Once);
            _mockPasswordStrengthService.VerifyNoOtherCalls();

            result.BreachCount.Should().Be(10);
            result.PasswordStrength.Should().Be(PasswordStrength.VeryStrong);
        }
    }
}
