using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class StreamType : CustomTypeString<StreamType>
    {
        public StreamType(string value) : base(value)
        {
        }
    }
}