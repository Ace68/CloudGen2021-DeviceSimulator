using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class EventId : CustomTypeString<EventId>
    {
        public EventId(string value) : base(value)
        {
        }
    }
}