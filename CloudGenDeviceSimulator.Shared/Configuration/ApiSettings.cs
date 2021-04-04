namespace CloudGenDeviceSimulator.Shared.Configuration
{
    public class ApiSettings
    {
        public MongoDbParameters MongoDbParameters { get; set; }
    }

    public class MongoDbParameters
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}