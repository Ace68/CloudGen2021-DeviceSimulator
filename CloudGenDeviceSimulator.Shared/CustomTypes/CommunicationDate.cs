using System;
using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator.Shared.CustomTypes
{
    public sealed class CommunicationDate : CustomTypeDate<CommunicationDate>
    {
        public CommunicationDate(DateTime value) : base(value)
        {
        }
    }
}