using System.Threading.Tasks;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public interface IStartup
    {
        Task DispatchDataAsync(int interval);
    }
}