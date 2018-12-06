using System;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Websocket.Relay
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

        public async Task Run(int port, RunMode mode)
        {
            await logger.Info($"Starting websocket relay in {mode.ToString()} mode on port {port.ToString()}");

            var websocketServer = new WebSocketServer(port);

            switch (mode)
            {
                case RunMode.Test:
                    websocketServer.AddWebSocketService<PulseOnConnectionWebsocketBehaviour>(
                        "/", x => x.SetLogger(this.logger));
                    break;
                default:
                    websocketServer.AddWebSocketService<RelayWebsocketBehaviour>(
                        "/", x => x.SetLogger(this.logger));
                    break;
            }

            try
            {
                websocketServer.Start();
            }
            catch (Exception ex)
            {
                await logger.Info("Could not start websocket server");
                await logger.Info(ex.Message);
            }
        }
    }
}
