using System;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.Shared.Abstracts;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenDeviceSimulator.Broker.Azure
{
    public sealed class Publish : IPublish
    {
        private readonly IServiceProvider _serviceProvider;

        public Publish(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task PublishDomainEventAsync<T>(T @event) where T : IDomainEvent
        {
            var domainEventProcessorAsync = this._serviceProvider.GetService<IDomainEventProcessorAsync<T>>();
            if (domainEventProcessorAsync == null)
            {
                Console.WriteLine($"[Publish.PublishAsync] - No DomainEventProcessor for {@event}");
                throw new Exception($"[Publish.PublishAsync] - No DomainEventProcessor for {@event}");
            }

            await domainEventProcessorAsync.PublishAsync(@event);
        }

        public async Task PublishIntegrationEventAsync<T>(T @event) where T : IntegrationEvent
        {
            var integrationEventProcessorAsync = this._serviceProvider.GetService<IIntegrationEventProcessorAsync<T>>();
            if (integrationEventProcessorAsync == null)
            {
                Console.WriteLine($"[Publish.PublishAsync] - No IntegrationEventProcessor for {@event}");
                throw new Exception($"[Publish.PublishAsync] - No IntegrationEventProcessor for {@event}");
            }

            await integrationEventProcessorAsync.PublishAsync(@event);
        }
    }
}