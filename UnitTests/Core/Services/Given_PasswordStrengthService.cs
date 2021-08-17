using Core.Interfaces;
using Core.Services.PasswordStrengthService;
using FluentAssertions;
using Models.Password;
using Xunit;

namespace UnitTests.Core.Services
{
    public class Given_PasswordStrengthService
    {
        private readonly IPasswordStrengthService _passwordStrengthService;

        public Given_PasswordStrengthService()
        {
            var configuration = Setup.GivenConfiguration();
            _passwordStrengthService = new PasswordStrengthService(configuration);
        }

        [Theory]
        [InlineData("", PasswordStrength.Blank)]
        [InlineData(" ", PasswordStrength.Blank)]
        [InlineData("          ", PasswordStrength.Blank)]
        [InlineData(null, PasswordStrength.Blank)]
        public async void When_PasswordIsBlank_Then_ReturnPasswordStrengthBlank(string password, PasswordStrength expectedPasswordStrength)
        {
            // Arrange
            // Act
            var result = await _passwordStrengthService.GetPasswordStrength(password);

            // Assert
            result.Should().Be(expectedPasswordStrength);
        }

        [Theory]
        [InlineData("P@sswo", false)]
        [InlineData("1234567", false)]
        [InlineData("abcdefgh", true)]
        [InlineData("12345abcdef", true)]
        public void When_HasMinimumLength_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasMinimumLength(password);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("aaaaa", false)]
        [InlineData("aabbaabbaabbaabb", false)]
        [InlineData("1122112211", false)]
        [InlineData("Abcda", true)]
        [InlineData("abcd#", true)]
        public void When_HasMinimumUniqueCharacters_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasMinimumUniqueCharacters(password);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("ABCDEFG", false)]
        [InlineData("12345", false)]
        [InlineData("#$%^&*", false)]
        [InlineData("AAAaAA", true)]
        [InlineData("abcde", true)]
        public void When_HasLowerCaseLetter_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasLowerCaseLetter(password);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("abcdefg", false)]
        [InlineData("12345", false)]
        [InlineData("#$%^&*", false)]
        [InlineData("aaaAaaa", true)]
        [InlineData("ABCDEFG", true)]
        public void When_HasUpperCaseLetter_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasUpperCaseLetter(password);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("abcdefg", false)]
        [InlineData("#$%^&*", false)]
        [InlineData("ABCDEFG", false)]
        [InlineData("abc3efg", true)]
        [InlineData("12345", true)]
        public void When_HasDigit_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasDigit(password);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("abcdefg", false)]
        [InlineData("12345", false)]
        [InlineData("AB12CDEFG", false)]
        [InlineData("#$%^&*", true)]
        [InlineData("abc$efg", true)]
        public void When_HasSpecialCharacter_Then_ReturnExpected(string password, bool expectedResult)
        {
            // Arrange
            // Act
            var result = _passwordStrengthService.HasSpecialCharacter(password);

            // Assert
            result.Should().Be(expectedResult);
        }

    }
}