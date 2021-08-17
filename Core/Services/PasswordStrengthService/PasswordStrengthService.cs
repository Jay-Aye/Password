using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Models.Password;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services.PasswordStrengthService
{
    public class PasswordStrengthService : IPasswordStrengthService
    {
        private readonly int MinLength = 8;
        private readonly int MinUniqueChars = 5;

        public PasswordStrengthService(IConfiguration configuration)
        {
            MinLength = int.Parse(configuration["PasswordMinimumLength"]);
            MinUniqueChars = int.Parse(configuration["PasswordMinimimUniqueCharacters"]);
        }

        public async Task<PasswordStrength> GetPasswordStrength(string password)
        {
            int passwordStrength = 0;

            if (string.IsNullOrWhiteSpace(password)) return PasswordStrength.Blank;
            if (HasMinimumLength(password)) passwordStrength++;
            if (HasMinimumUniqueCharacters(password)) passwordStrength++;
            if (HasLowerCaseLetter(password)) passwordStrength++;
            if (HasUpperCaseLetter(password)) passwordStrength++;
            if (HasDigit(password)) passwordStrength++;
            if (HasSpecialCharacter(password)) passwordStrength++;

            return (PasswordStrength)passwordStrength;
        }

        public bool HasMinimumLength(string password)
        {
            return password.Length >= MinLength;
        }

        public bool HasMinimumUniqueCharacters(string password)
        {
            return password.Distinct().Count() >= MinUniqueChars;
        }

        public bool HasLowerCaseLetter(string password)
        {
            var result = password.Any(x => char.IsLower(x));
            return result;
        }

        public bool HasUpperCaseLetter(string password)
        {
            return password.Any(x => char.IsUpper(x));
        }

        public bool HasDigit(string password)
        {
            return password.Any(x => char.IsDigit(x));
        }

        public bool HasSpecialCharacter(string password)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            return rgx.IsMatch(password);
        }
    }
}
