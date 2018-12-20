using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Websocket.Relay
{
    public class PulseOnConnectionWebsocketBehaviour : WebSocketBehavior
    {
        private static Object _lock;
        private bool _testHasBegun;
        private ILogger logger;
        private Limiter limiter;

        static PulseOnConnectionWebsocketBehaviour()
        {
            _lock = new object();
        }

        public PulseOnConnectionWebsocketBehaviour()
        {
            _testHasBegun = false;

            limiter = new Limiter(60); // Default max frames per second;
        }

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void SetMaxFramesPerSecond(double maxFramesPerSecond)
        {
            if (maxFramesPerSecond < 15 || maxFramesPerSecond > 120)
            {
                throw new ArgumentException("Frames per second should be between 15 and 120 inclusive");
            }

            this.limiter = new Limiter(maxFramesPerSecond);
        }

        protected override void OnOpen()
        {
            if (!_testHasBegun)
            {
                lock (_lock)
                {
                    _testHasBegun = true;
                }

                LogInfo($"Connected to client!")
                    .GetAwaiter()
                    .GetResult();

                SendBinaryPulseForXSeconds(10, 0.5)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public async Task SendBinaryPulseForXSeconds(double numberOfSeconds, double flashInterval)
        {
            await LogInfo($"Sending pulses of white light for {numberOfSeconds.ToString()}s with {flashInterval.ToString()}s intervals");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            bool isOn = false;
            while (stopwatch.Elapsed.TotalSeconds < numberOfSeconds)
            {
                var shouldBeOn = (int)(stopwatch.Elapsed.TotalSeconds / flashInterval) % 2 == 0;

                if ((isOn & !shouldBeOn) || (!isOn & shouldBeOn))
                {
                    var status = shouldBeOn ? "on" : "off";
                    await LogInfo($"Time elapsed: {Math.Round(stopwatch.Elapsed.TotalSeconds, 2).ToString()}s. Toggling pulse to be {status}");
                }

                isOn = shouldBeOn;

                var pulse = ConstructPulse(30, isOn);

                limiter.Limit(() => Sessions.Broadcast(pulse));
            }

            stopwatch.Stop();
        }

        private static byte[] ConstructPulse(int numberOfBytes, bool toggleOn)
        {
            var pulse = new byte[numberOfBytes];

            for (int i = 0; i < numberOfBytes; i++)
            {
                pulse[i] = toggleOn ? (byte)255 : (byte)0;
            }

            return pulse;
        }

        private async Task LogInfo(string message)
        {
            if (logger != null)
            {
                await logger.Info(message);
            }
        }
    }
}
