using System.Threading.Tasks;
using FourSolid.Athena.Messages.Events;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public interface IPublish
    {
        Task PublishDomainEventAsync<T>(T @event) where T : IDomainEvent;
        Task PublishIntegrationEventAsync<T>(T @event) where T : IntegrationEvent;
    }
}