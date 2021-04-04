using System.Collections.Generic;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;

namespace CloudGenDeviceSimulator.ReadModel.Abstracts
{
    public interface IEventStoreServices
    {
        Task AppendEventAsync<T>(EventId eventId, StreamType streamType, StreamData streamData, DeviceId aggregateId,
            DeviceName aggregateName, StreamWhen streamWhen) where T : EventStoreBase;

        Task SetEventToDispatched<T>(EventId eventId) where T : EventStoreBase;

        Task<IEnumerable<ThermometerEventStoreJson>> GetEventsNotDispatchedAsync<T>() where T : EventStoreBase;
    }
}