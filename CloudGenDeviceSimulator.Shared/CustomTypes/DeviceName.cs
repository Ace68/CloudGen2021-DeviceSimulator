using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class DeviceName : CustomTypeString<DeviceName>
    {
        public DeviceName(string value) : base(value)
        {
        }
    }
}