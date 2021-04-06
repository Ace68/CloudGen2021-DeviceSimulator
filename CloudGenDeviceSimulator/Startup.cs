using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.ReadModel.Dtos;
using CloudGenDeviceSimulator.Shared.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using CloudGenDeviceSimulator.Shared.JsonModel;
using Newtonsoft.Json;

namespace CloudGenDeviceSimulator
{
    public sealed class Startup : IStartup
    {
        private readonly IThermometerServices _thermometerServices;
        private readonly IEventStoreServices _eventStoreServices;
        private readonly IPublish _publish;
        
        private readonly Timer _timer = new();

        public Startup(IThermometerServices thermometerServices,
            IEventStoreServices eventStoreServices,
            IPublish publish)
        {
            this._thermometerServices = thermometerServices;
            this._eventStoreServices = eventStoreServices;
            this._publish = publish;
        }

        public Task DispatchDataAsync(int interval)
        {
            this.ReadAndPublishThermometerValuesAsync().GetAwaiter().GetResult();
            
            this._timer.Elapsed += this.TimerElapsed;
            this._timer.Interval = interval * 60 * 1000;
            this._timer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the process.");
            while (Console.Read() != 'q')
            {
            }
            
            return Task.CompletedTask;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this._timer.Enabled = false;

            this.ReadAndPublishThermometerValuesAsync().GetAwaiter().GetResult();
            
            this._timer.Enabled = true;
        }

        private async Task ReadAndPublishThermometerValuesAsync()
        {
            var temperature = this._thermometerServices.ReadThermometerValues();
            var thermometerValues = this._thermometerServices.MapToThermometerValuesUpdated(temperature);

            await this.AppendValuesIntoEventStoreAsync(thermometerValues);
            await this.PublishAsync<ThermometerEventStore>();
        }

        private async Task AppendValuesIntoEventStoreAsync(IEnumerable<ThermometerValuesUpdated> thermometerValues)
        {
            try
            {
                foreach (var thermometerValuesUpdated in thermometerValues)
                {
                    await this._eventStoreServices.AppendEventAsync<ThermometerEventStore>(thermometerValuesUpdated.EventId,
                        new StreamType(nameof(ThermometerValuesUpdated)),
                        new StreamData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(thermometerValuesUpdated))),
                        thermometerValuesUpdated.DeviceId,
                        thermometerValuesUpdated.DeviceName,
                        new StreamWhen(thermometerValuesUpdated.CommunicationDate.Value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task PublishAsync<T>() where T : EventStoreBase
        {
            var eventsToDispatch = await this._eventStoreServices.GetEventsNotDispatchedAsync<T>();
            var domainEvents = eventsToDispatch as ThermometerEventStoreJson[] ?? eventsToDispatch.ToArray();

            Console.WriteLine($"Start Dispatch {nameof(T)} event");
            foreach (var domainEvent in domainEvents)
            {
                var eventToDispatch = DeserializeEvent(domainEvent.StreamData);
                if (eventToDispatch == null)
                    continue;

                await this._publish.PublishDomainEventAsync(eventToDispatch);
                await this._eventStoreServices.SetEventToDispatched<T>(new Shared.CustomTypes.EventId(domainEvent.EventId));
            }
            Console.WriteLine($"Events {nameof(T)} Dispatched");
        }

        /// <summary>
        /// Deserializes the event from the raw EccentricitaProconEventStore event to my event.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static ThermometerValuesUpdated DeserializeEvent(byte[] data)
        {
            try
            {
                return JsonConvert.DeserializeObject<ThermometerValuesUpdated>(Encoding.UTF8.GetString(data));
            }
            catch
            {
                return null;
            }
        }
    }
}