using System;
using System.Threading.Tasks;
using System.Timers;
using CloudGenDeviceSimulator.Shared.Abstracts;

namespace CloudGenDeviceSimulator
{
    public sealed class Startup : IStartup
    {
        private readonly Timer _timer = new Timer();
        
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

            this._timer.Enabled = true;
        }
    }
}