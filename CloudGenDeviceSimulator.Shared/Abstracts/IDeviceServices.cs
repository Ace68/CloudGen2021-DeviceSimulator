using System.Collections.Generic;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.Shared.JsonModel;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public interface IDeviceServices
    {
        Task<IEnumerable<DeviceJson>> GetDevicesAsync();
    }
}