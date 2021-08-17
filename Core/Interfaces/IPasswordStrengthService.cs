using Models.Password;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPasswordStrengthService
    {
        Task<PasswordStrength> GetPasswordStrength(string password);
        bool HasMinimumLength(string password);
        bool HasMinimumUniqueCharacters(string password);
        bool HasLowerCaseLetter(string password);
        bool HasUpperCaseLetter(string password);
        bool HasDigit(string password);
        bool HasSpecialCharacter(string password);
    }
}
