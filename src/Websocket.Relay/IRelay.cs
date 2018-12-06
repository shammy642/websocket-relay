using System.Threading.Tasks;

namespace Websocket.Relay
{
    public interface IRelay
    {
        Task Run(int port, RunMode mode);
    }
}
