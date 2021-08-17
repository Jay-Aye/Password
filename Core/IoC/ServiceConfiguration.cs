using Core.Interfaces;
using Core.Services.PasswordService;
using Core.Services.PasswordStrengthService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IoC
{
    public static class CoreServiceConfiguration
    {
        public static void ConfigureCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IPasswordStrengthService, PasswordStrengthService>();
            services.AddSingleton(provider => configuration);
        }
    }
}
