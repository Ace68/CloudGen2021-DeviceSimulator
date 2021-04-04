using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.ReadModel.MongoDb.Repository;
using CloudGenDeviceSimulator.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CloudGenDeviceSimulator.ReadModel.MongoDb
{
    public static class MongoDbHelpers
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbParameters mongoDbParameter)
        {
            services.AddSingleton<IMongoClient>(provider => new MongoClient(mongoDbParameter.ConnectionString));
            services.AddScoped<IMongoDatabase>(provider =>
                provider.GetService<IMongoClient>().GetDatabase(mongoDbParameter.DatabaseName));

            services.AddScoped<IPersister, Persister>();

            return services;
        }
    }
}