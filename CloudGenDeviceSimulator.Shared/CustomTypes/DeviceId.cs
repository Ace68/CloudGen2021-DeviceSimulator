using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class DeviceId : CustomTypeString<DeviceId>
    {
        public DeviceId(string value) : base(value)
        {
        }
    }
}