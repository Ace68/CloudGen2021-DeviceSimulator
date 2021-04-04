using System;

namespace CloudGenDeviceSimulator.Shared.JsonModel
{
    public class ThermometerEventStoreJson
    {
        public string MessageId { get; set; }
        public string StreamType { get; set; }
        public byte[] StreamData { get; set; }
        public string AggregateId { get; set; }
        public string AggregateName { get; set; }
        public DateTime StreamWhen { get; set; }
        public bool IsDispatched { get; set; }
    }
}