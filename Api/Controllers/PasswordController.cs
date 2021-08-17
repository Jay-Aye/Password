using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Password;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet]
        [Route("{password}")]
        public async Task<PasswordStrengthResponse> Get(string password)
        {             
            var response = await _passwordService.GetPasswordDetails(password);

            return response;
        }
    }
}
