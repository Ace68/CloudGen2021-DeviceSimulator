using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.ApplicationServices.Concretes;
using CloudGenDeviceSimulator.Broker.Azure;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenDeviceSimulator.Broker.Helpers
{
    public static class ApplicationHelper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEventStoreServices, EventStoreServices>();
            services.AddScoped<IThermometerServices, ThermometerServices>();
            services.AddScoped<IDeviceServices, DeviceServices>();
            services.AddScoped<IPublish, Publish>();
            
            return services;
        }
    }
}