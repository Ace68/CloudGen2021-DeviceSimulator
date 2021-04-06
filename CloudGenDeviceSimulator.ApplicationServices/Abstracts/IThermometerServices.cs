using System.Collections.Generic;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;

namespace CloudGenDeviceSimulator.ApplicationServices.Abstracts
{
    public interface IThermometerServices
    {
        IEnumerable<Temperature> ReadThermometerValues();

        IEnumerable<ThermometerValuesUpdated> MapToThermometerValuesUpdated(IEnumerable<Temperature> temperature,
            DeviceJson device);
    }
}