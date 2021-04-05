using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CloudGenDeviceSimulator.ApplicationServices.Abstracts;
using CloudGenDeviceSimulator.Messages.Events;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.ReadModel.Dtos;
using CloudGenDeviceSimulator.Shared.Abstracts;
using CloudGenDeviceSimulator.Shared.CustomTypes;
using Newtonsoft.Json;

namespace CloudGenDeviceSimulator
{
    public sealed class Startup : IStartup
    {
        private readonly IThermometerServices _thermometerServices;
        private readonly IEventStoreServices _eventStoreServices;
        
        private readonly Timer _timer = new Timer();

        public Startup(IThermometerServices thermometerServices,
            IEventStoreServices eventStoreServices)
        {
            this._thermometerServices = thermometerServices;
            this._eventStoreServices = eventStoreServices;
        }

        public Task DispatchDataAsync(int interval)
        {
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

            var temperature = this._thermometerServices.ReadThermometerValues();
            var thermometerValues = this._thermometerServices.MapToThermometerValuesUpdated(temperature);
            this.AppendValuesIntoEventStoreAsync(thermometerValues).GetAwaiter().GetResult();
            
            this._timer.Enabled = true;
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
    }
}