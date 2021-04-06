using System;
using System.Collections.Generic;
using System.Linq;
using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using FourSolid.Common.ValueObjects;
using Microsoft.Extensions.Logging;
using EventId = CloudGenDeviceSimulator.Shared.CustomTypes.EventId;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public sealed class ThermometerServices : BaseServices, IThermometerServices
    {
        private readonly AccountInfo _who;
        private readonly Random _random = new Random();
        
        public ThermometerServices(IPersister persister, ILoggerFactory loggerFactory)
            : base(persister, loggerFactory)
        {
            this._who = new AccountInfo(new AccountId(Guid.NewGuid().ToString()),
                new AccountName("GloabAzureThermometer"),
                new AccountRole("D"));
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
            return (from t in temperature
                let reference = DateTime.UtcNow.AddMilliseconds(this._random.Next(1,50))
                select new ThermometerValuesUpdated(new DeviceId(Guid.NewGuid()),
                    new EventId(
                        $"{reference.Year:0000}{reference.Month:00}{reference.Day:00}{reference.Hour:00}{reference.Minute:00}{reference.Second:00}{reference.Millisecond:000}"),
                    new DeviceName("GlobalAzureThermometer"), t, new UnitOfMeasurement("F"),
                    new CommunicationDate(DateTime.UtcNow), this._who, new When(DateTime.UtcNow))).ToList();
        }
    }
}