﻿using System.IO;
using CloudGenDeviceSimulator.ReadModel.MongoDb;
using CloudGenDeviceSimulator.Shared.Abstracts;
using CloudGenDeviceSimulator.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CloudGenDeviceSimulator
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            serviceCollection.AddOptions();

            serviceCollection.Configure<ApiSettings>(options =>
                _configuration.GetSection("CloudGen").Bind(options));

            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs\\CloudGen.txt")
                .CreateLogger();

            var startup = serviceProvider.GetService<IStartup>();
            int.TryParse(_configuration["CloudGen:Timer:Interval"], out var interval);
            startup?.DispatchDataAsync(interval).GetAwaiter().GetResult();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStartup, Startup>();
            services.AddLogging();

            var mongoDbParameters = new MongoDbParameters();
            _configuration.GetSection("CloudGen:MongoDbParameters").Bind(mongoDbParameters);
            services.AddMongoDb(mongoDbParameters);

            return services;
        }
    }
}
