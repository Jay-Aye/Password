using Core.Interfaces;
using Models.Password;
using System.Threading.Tasks;

namespace Core.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordBreachClient _passwordBreachClient;
        private readonly IPasswordStrengthService _passwordStrengthService;

        public PasswordService(IPasswordBreachClient passwordBreachClient, IPasswordStrengthService passwordStrengthService)
        {
            _passwordBreachClient = passwordBreachClient;
            _passwordStrengthService = passwordStrengthService;
        }

        public async Task<PasswordStrengthResponse> GetPasswordDetails(string password)
        {
            PasswordStrengthResponse result = new PasswordStrengthResponse();

            result.PasswordStrength = await _passwordStrengthService.GetPasswordStrength(password);
            result.BreachCount = await _passwordBreachClient.GetPasswordBreachCount(password);

            return result;
        }
    }
}
