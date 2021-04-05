using System;
using System.Collections.Generic;
using System.Linq;
using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using Microsoft.Extensions.Logging;
using EventId = CloudGenDeviceSimulator.Shared.CustomTypes.EventId;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public sealed class ThermometerServices : BaseServices, IThermometerServices
    {
        public ThermometerServices(IPersister persister, ILoggerFactory loggerFactory)
            : base(persister, loggerFactory)
        {
        }
        
        public IEnumerable<Temperature> ReadThermometerValues()
        {
            var random = new Random();

            var temperature = new List<Temperature>();
            for (var i = 0; i < 10; i++)
            {
                temperature.Add(new Temperature(random.Next(64, 76)));
            }

            return temperature;
        }

        public IEnumerable<ThermometerValuesUpdated> MapToThermometerValuesUpdated(IEnumerable<Temperature> temperature)
        {
            var reference = DateTime.UtcNow;
            return temperature.Select(t => new ThermometerValuesUpdated(new EventId($"{reference.Year:0000}{reference.Month:00}{reference.Day:00}{reference.Hour:00}{reference.Minute:00}{reference.Second:00}{reference.Millisecond:000}"),
                new DeviceId(Guid.NewGuid().ToString()),
                new DeviceName("GlobalAzureThermometer"),
                t,
                new UnitOfMeasurement("F"),
                new CommunicationDate(DateTime.UtcNow)));
        }
    }
}