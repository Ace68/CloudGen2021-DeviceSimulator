using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.ReadModel.Dtos;
using CloudGenDeviceSimulator.Shared.Abstracts;
using CloudGenDeviceSimulator.Shared.JsonModel;
using CloudGenDeviceSimulator.Shared.Services;
using Microsoft.Extensions.Logging;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public sealed class DeviceServices : BaseServices, IDeviceServices
    {
        public DeviceServices(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
        {
        }

        public async Task<IEnumerable<DeviceJson>> GetDevicesAsync()
        {
            try
            {
                var deviceDto = await this.Persister.FindAsync<Device>();
                var deviceArray = deviceDto as Device[] ?? deviceDto.ToArray();
                
                return deviceArray.Any()
                    ? deviceArray.Select(dto => dto.ToJson())
                    : Enumerable.Empty<DeviceJson>();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }
    }
}