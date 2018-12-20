using System;
using System.Threading.Tasks;

namespace Websocket.Relay
{
    internal class Limiter
    {
        private DateTime lastSend;

        internal double MaxPerSecond { get; }

        internal bool ShouldThrottle => DateTime.UtcNow.Subtract(lastSend).TotalMilliseconds < (1000d / MaxPerSecond);

        internal Limiter(double maxPerSecond)
        {
            MaxPerSecond = maxPerSecond;

            this.lastSend = DateTime.MinValue;
        }

        internal void Limit(Action action)
        {
            if (ShouldThrottle)
            {
                return;
            }

            action();
            this.lastSend = DateTime.UtcNow;
        }

        internal async Task LimitAsync(Func<Task> action)
        {
            if (ShouldThrottle)
            {
                return;
            }

            await action();
            this.lastSend = DateTime.UtcNow;
        }
    }
}
