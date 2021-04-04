namespace CloudGenDeviceSimulator.ReadModel.Abstracts
{
    public abstract class DtoBase : IDtoBase
    {
        public string Id { get; protected set; }
        public bool IsDeleted { get; protected set; }
    }
}