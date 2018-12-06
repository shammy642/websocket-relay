using System.Threading.Tasks;

namespace Websocket.Relay
{
    public interface ILogger
    {
        Task Info(string message);
    }
}
