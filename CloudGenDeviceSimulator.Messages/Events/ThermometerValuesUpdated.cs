using System;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using FourSolid.Athena.Messages.Events;

namespace CloudGenDeviceSimulator.Messages.Events
{
    public sealed class ThermometerValuesUpdated : IntegrationEvent
    {
        public readonly EventId EventId;
        public readonly DeviceId DeviceId;
        public readonly DeviceName DeviceName;
        public readonly Temperature Temperature;
        public readonly UnitOfMeasurement UnitOfMeasurement;
        public readonly CommunicationDate CommunicationDate;

        public ThermometerValuesUpdated(EventId eventId, DeviceId deviceId, DeviceName deviceName,
            Temperature temperature, UnitOfMeasurement unitOfMeasurement, CommunicationDate communicationDate) : base(
            Guid.NewGuid())
        {
            this.EventId = eventId;
            
            this.DeviceId = deviceId;
            this.DeviceName = deviceName;
            this.Temperature = temperature;
            this.UnitOfMeasurement = unitOfMeasurement;
            this.CommunicationDate = communicationDate;
        }
    }
}