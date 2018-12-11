using System.Collections.Generic;
using System.Threading.Tasks;

namespace Websocket.Relay.Tests
{
    public class MessageCollector : ILogger
    {
        private readonly List<string> _messages;

        public IEnumerable<string> Messages => _messages;

        public MessageCollector()
        {
            _messages = new List<string>();
        }

        public async Task Info(string message)
        {
            await Task.Yield();
            _messages.Add(message);
        }
    }
}
