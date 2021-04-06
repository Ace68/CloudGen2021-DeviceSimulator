using CloudGenDeviceSimulator.Messages.Events;
using FourSolid.Athena.Messages;
using FourSolid.Mercurio.Azure.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenDeviceSimulator.Broker.Helpers
{
    public static class DeviceMessagesHelper
    {
        public static IServiceCollection AddDeviceMessagesProcessor(this IServiceCollection services, string iotConnectionString)
        {
            services.AddScoped(provider =>
            {
                var brokerOptions = new BrokerOptions
                {
                    ConnectionString = string.Empty,
                    SubscriptionName = iotConnectionString
                };

                var iotMessageProcessorFactory =
                    new IoTEventProcessorFactory<ThermometerValuesUpdated>(brokerOptions);
                return iotMessageProcessorFactory.DomainEventProcessorAsync;
            });

            return services;
        }
    }
}