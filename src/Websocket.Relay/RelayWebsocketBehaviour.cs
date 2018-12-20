using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Websocket.Relay
{
    public class RelayWebsocketBehaviour : WebSocketBehavior
    {
        private ILogger logger;
        private Limiter limiter;

        public RelayWebsocketBehaviour()
        {
            limiter = new Limiter(60); // Default max frames per second
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

        protected override void OnMessage(MessageEventArgs e)
        {
            limiter.Limit(() => Sessions.Broadcast(e.Data));
        }
    }
}
