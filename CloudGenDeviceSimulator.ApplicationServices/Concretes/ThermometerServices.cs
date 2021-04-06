using System;
using System.Collections.Generic;
using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;
using CloudGenDeviceSimulator.Shared.Services;
using FourSolid.Common.ValueObjects;
using Microsoft.Extensions.Logging;
using EventId = CloudGenDeviceSimulator.Shared.CustomTypes.EventId;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public sealed class ThermometerServices : BaseServices, IThermometerServices
    {
        private readonly AccountInfo _who;
        
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

        public IEnumerable<ThermometerValuesUpdated> MapToThermometerValuesUpdated(IEnumerable<Temperature> temperature, DeviceJson device)
        {
            var thermometerValues = new List<ThermometerValuesUpdated>();
            var reference = DateTime.UtcNow;
            foreach (var t in temperature)
            {
                thermometerValues.Add(new ThermometerValuesUpdated(new DeviceId(device.DeviceId.ToGuid()),
                    new EventId(
                        $"{reference.Year:0000}{reference.Month:00}{reference.Day:00}{reference.Hour:00}{reference.Minute:00}{reference.Second:00}{reference.Millisecond:000}"),
                    new DeviceName(device.DeviceName), t, new UnitOfMeasurement("F"),
                    new CommunicationDate(DateTime.UtcNow), this._who, new When(DateTime.UtcNow)));

                reference = reference.AddMilliseconds(10);
            }

            return thermometerValues;
        }
    }
}