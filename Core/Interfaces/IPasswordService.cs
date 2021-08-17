using Models.Password;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPasswordService
    {
        Task<PasswordStrengthResponse> GetPasswordDetails(string password);
    }
}
