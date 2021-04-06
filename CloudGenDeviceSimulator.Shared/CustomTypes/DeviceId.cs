using System;
using FourSolid.Athena.Core;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class DeviceId : DomainId
    {
        public DeviceId(Guid value) : base(value)
        {
        }
    }
}