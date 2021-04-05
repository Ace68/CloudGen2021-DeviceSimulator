using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class UnitOfMeasurement : CustomTypeString<UnitOfMeasurement>
    {
        public UnitOfMeasurement(string value) : base(value)
        {
        }
    }
}