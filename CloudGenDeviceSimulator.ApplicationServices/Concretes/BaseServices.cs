using CloudGenDeviceSimulator.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public abstract class BaseServices
    {
        protected IPersister Persister;
        protected ILogger Logger;

        protected BaseServices(IPersister persister, ILoggerFactory loggerFactory)
        {
            this.Persister = persister;
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}