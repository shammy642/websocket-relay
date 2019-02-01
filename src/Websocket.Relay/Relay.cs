using System;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Websocket.Relay
{
    public class Relay : IRelay
    {
        private readonly ILogger logger;

        public static IRelay Load(ILogger logger, RelayConfiguration configuration)
        {
            // TODO: Initialize dependencies
            return new Relay(logger, configuration);
        }

        readonly RelayConfiguration configuration;

        private Relay(ILogger logger, RelayConfiguration configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task Run(int port, RunMode mode)
        {
            await logger.Info($"Starting websocket relay relay relay relay in {mode.ToString()} mode on port {port.ToString()}");

            var websocketServer = new WebSocketServer(port);

            switch (mode)
            {
                case RunMode.Test:
                    websocketServer.AddWebSocketService<PulseOnConnectionWebsocketBehaviour>(
                        "/", x => 
                        {
                            x.SetLogger(this.logger);
                            x.SetMaxFramesPerSecond(configuration.MaxFramesPerSecond);
                        });
                    break;
                default:
                    websocketServer.AddWebSocketService<RelayWebsocketBehaviour>(
                        "/", x =>
                        {
                            x.SetLogger(this.logger);
                            x.SetMaxFramesPerSecond(configuration.MaxFramesPerSecond);
                        });
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
