using Core.Interfaces;
using Core.Services.Http;
using Infrastructure.Clients.PasswordBreach;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Infrastructure.IoC
{
    public static class CoreServiceConfiguration
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddTransient<IPasswordBreachClient, PasswordBreachClient>();
            services.AddSingleton(provider => configuration);
        }
    }
}
