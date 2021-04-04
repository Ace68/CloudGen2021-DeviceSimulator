using System;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;

namespace CloudGenDeviceSimulator.ReadModel.Abstracts
{
    public abstract class EventStoreBase : DtoBase
    {
        public string StreamType { get; private set; }
        public byte[] StreamData { get; private set; }
        public string AggregateId { get; private set; }
        public string AggregateName { get; private set; }
        public DateTime StreamWhen { get; private set; }
        public bool IsDispatched { get; private set; }

        protected EventStoreBase(EventId eventId, StreamType streamType, StreamData streamData, DeviceId aggregateId,
            DeviceName aggregateName, StreamWhen streamWhen)
        {
            this.Id = eventId.Value;

            this.StreamType = streamType.Value;
            this.StreamData = streamData.Value;
            this.AggregateId = aggregateId.ToString();
            this.AggregateName = aggregateName.Value;
            this.StreamWhen = streamWhen.Value;

            this.IsDispatched = false;
        }

        public void SetEventDispatched()
        {
            this.IsDispatched = true;
        }

        public ThermometerEventStoreJson ToJson()
        {
            return new ThermometerEventStoreJson
            {
                MessageId = this.Id,
                StreamData = this.StreamData,
                StreamWhen = this.StreamWhen,
                IsDispatched = this.IsDispatched
            };
        }
    }
}