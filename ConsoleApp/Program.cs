using Core.Interfaces;
using Core.Services.PasswordService;
using Core.Services.PasswordStrengthService;
using Infrastructure.Clients.PasswordBreach;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ConsoleApp
{
    public class Program
    {
        private static IPasswordService _passwordService;

        public static void Main(string[] args)
        {
            Setup();
            
            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();

            var result = _passwordService.GetPasswordDetails(password).Result;

            Console.WriteLine();
            Console.WriteLine($"Your password strength is : {result.PasswordStrength}");
            Console.WriteLine($"Your password has appeared in {result.BreachCount} breaches");
            Console.ReadLine();
        }

        static void Setup()
        {
            var configuration = GivenConfiguration();
            var services = Startup.ConfigureServices(configuration);
            var serviceProvider = services.BuildServiceProvider();

            _passwordService = serviceProvider.GetService<IPasswordService>();
        }

        public static IConfiguration GivenConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

    }
}
