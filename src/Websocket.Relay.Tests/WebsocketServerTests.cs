using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using Xunit;

namespace Websocket.Relay.Tests
{
    public class WebsocketServerTests
    {
        [Fact]
        public async Task WebsocketRelay_WhenInTestMode_SendsMessagesRegardingPulses()
        {
            var messageCollector = new MessageCollector();
            var relay = Relay.Load(messageCollector);
            await relay.Run(7890, RunMode.Test);

            var ws = new WebSocket("ws://localhost:7890");
            ws.Connect();

            Thread.Sleep(10000);

            Assert.Contains("pulse", messageCollector.Messages
                .Aggregate((prev, curr) => prev + curr)
                .ToLowerInvariant());
        }
    }
}
