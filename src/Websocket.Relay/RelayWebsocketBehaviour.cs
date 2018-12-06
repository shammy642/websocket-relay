using WebSocketSharp;
using WebSocketSharp.Server;

namespace Websocket.Relay
{
    public class RelayWebsocketBehaviour : WebSocketBehavior
    {
        private ILogger logger;

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data);
        }
    }
}
