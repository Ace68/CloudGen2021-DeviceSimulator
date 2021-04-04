using System;
using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class StreamWhen : CustomTypeDate<StreamWhen>
    {
        public StreamWhen(DateTime value) : base(value)
        {
        }
    }
}