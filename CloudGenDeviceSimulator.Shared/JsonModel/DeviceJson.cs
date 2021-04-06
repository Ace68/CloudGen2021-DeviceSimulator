namespace CloudGenDeviceSimulator.Shared.JsonModel
{
    public class DeviceJson
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public DeviceTypeJson DeviceType { get; set; }
        public bool IsEnabled { get; set; }
    }
}