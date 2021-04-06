using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.JsonModel;

namespace CloudGenDeviceSimulator.ReadModel.Dtos
{
    public class Device : DtoBase
    {
        public string DeviceId { get; private set; }
        public string DeviceName { get; private set; }
        public string SerialNumber { get; private set; }
        public DeviceTypeJson DeviceType { get; private set; }
        public bool IsEnabled { get; private set; }
        
        protected Device()
        { }

        public DeviceJson ToJson()
        {
            return new DeviceJson
            {
                DeviceId = this.DeviceId,
                DeviceName = this.DeviceName,
                SerialNumber = this.SerialNumber,
                DeviceType = this.DeviceType,
                IsEnabled = this.IsEnabled
            };
        }
    }
}