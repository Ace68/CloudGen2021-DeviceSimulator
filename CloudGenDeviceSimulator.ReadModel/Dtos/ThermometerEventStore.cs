using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;

namespace CloudGenDeviceSimulator.ReadModel.Dtos
{
    public class ThermometerEventStore : EventStoreBase
    {
        public ThermometerEventStore(EventId eventId, StreamType streamType, StreamData streamData,
            DeviceId aggregateId, DeviceName aggregateName, StreamWhen streamWhen) : base(eventId, streamType,
            streamData, aggregateId, aggregateName, streamWhen)
        {
        }
    }
}