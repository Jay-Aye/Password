using Core.IoC;
using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.IoC
{
    public static class ConsoleServiceConfiguration
    {
        public static ServiceProvider Init(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices(configuration);
            return serviceCollection.BuildServiceProvider();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureCoreServices(configuration);
            services.ConfigureInfrastructureServices(configuration);
        }
    }
}
