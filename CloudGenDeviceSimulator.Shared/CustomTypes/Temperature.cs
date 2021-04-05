using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class Temperature : CustomTypeBase<Temperature>
    {
        public readonly double Value;

        public Temperature(double value)
        {
            this.Value = value;
        }
    }
}