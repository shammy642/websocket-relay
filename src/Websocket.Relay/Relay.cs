using System;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Fadecandy.Relay
{
    public class Relay : IRelay
    {
        private readonly ILogger logger;

        public static IRelay Load(ILogger logger)
        {
            // TODO: Initialize dependencies
            return new Relay(logger);
        }

        private Relay(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Run(RunMode mode)
        {
            await logger.Info($"Starting Fadecandy websocket relay in {mode.ToString()} mode");

            // TODO: Implement server
        }
    }
}
