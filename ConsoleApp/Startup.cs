using ConsoleApp.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.ConfigureServices(configuration);

            return serviceCollection;
        }
    }
}
