using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPasswordBreachClient
    {
        Task<int> GetPasswordBreachCount(string password);
    }
}
