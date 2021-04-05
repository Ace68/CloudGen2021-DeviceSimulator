using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;
using FourSolid.Common.Services;
using Microsoft.Extensions.Logging;
using EventId = CloudGenDeviceSimulator.Shared.CustomTypes.EventId;

namespace CloudGenDeviceSimulator.ApplicationServices.Concretes
{
    public class EventStoreServices : BaseServices, IEventStoreServices
    {
        public EventStoreServices(IPersister persister, ILoggerFactory loggerFactory)
            : base(persister, loggerFactory)
        {
        }

        public async Task AppendEventAsync<T>(EventId eventId, StreamType streamType, StreamData streamData,
            DeviceId aggregateId, DeviceName aggregateName, StreamWhen streamWhen) where T : EventStoreBase
        {
            try
            {
                var streamEvent =
                    await this.Persister.GetByIdAsync<T>(eventId.GetValue());
                if (streamEvent != null && !string.IsNullOrEmpty(streamEvent.Id))
                    return;

                streamEvent = ConstructAggregate<T>(eventId, streamType, streamData, aggregateId, aggregateName, streamWhen);
                await this.Persister.InsertAsync(streamEvent);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw new Exception(CommonServices.GetErrorMessage(ex));
            }
        }

        public async Task SetEventToDispatched<T>(EventId eventId) where T : EventStoreBase
        {
            try
            {
                var streamEvent = await this.Persister.GetByIdAsync<T>(eventId.GetValue());
                if (streamEvent == null || string.IsNullOrWhiteSpace(streamEvent.Id))
                    return;

                streamEvent.SetEventDispatched();
                await this.Persister.UpdateAsync(streamEvent);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw new Exception(CommonServices.GetErrorMessage(ex));
            }
        }

        public async Task<IEnumerable<ThermometerEventStoreJson>> GetEventsNotDispatchedAsync<T>() where T : EventStoreBase
        {
            try
            {
                var streamEvents =
                    await this.Persister.FindAsync<T>(s => !s.IsDispatched);

                var eventsArray = streamEvents as T[] ?? streamEvents.ToArray();
                return eventsArray.Any()
                    ? eventsArray.Select(dto => dto.ToJson())
                    : Enumerable.Empty<ThermometerEventStoreJson>();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw new Exception(CommonServices.GetErrorMessage(ex));
            }
        }

        private static T ConstructAggregate<T>(EventId eventId, StreamType streamType, StreamData streamData,
            DeviceId aggregateId, DeviceName aggregateName, StreamWhen streamWhen)
        {
            return (T) Activator.CreateInstance(typeof(T), eventId, streamType, streamData, aggregateId, aggregateName,
                streamWhen);
        }
    }
}