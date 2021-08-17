using Core.Utils;
using FluentAssertions;
using Xunit;

namespace UnitTests.Core.Utils
{
    public class Given_HashingExtensions
    {
        [Theory]
        [InlineData("password", "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8")]
        [InlineData("&*#$%^&", "42D0CEE3F194846EBB7799D3EC1E36DB29971070")]
        [InlineData("P@ssWorD", "6273CB7F65DA8B97CDF35BD21DA8B46D45AFD57A")]
        [InlineData("12345678910", "9048EAD9080D9B27D6B2B6ED363CBF8CCE795F7F")]
        public void When_Hash_Then_GenerateHashOfInput(string input, string expectedResult)
        {
            // Arrange
            // Act
            var result = input.Hash();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
