using System.Collections.Generic;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.Shared.CustomTypes;

namespace CloudGenDeviceSimulator.ApplicationServices.Abstracts
{
    public interface IThermometerServices
    {
        IEnumerable<Temperature> ReadThermometerValues();
        IEnumerable<ThermometerValuesUpdated> MapToThermometerValuesUpdated(IEnumerable<Temperature> temperature);
    }
}