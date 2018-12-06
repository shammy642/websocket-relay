using System.Threading.Tasks;

namespace Websocket.Relay
{
    public interface IRelay
    {
        Task Run(RunMode mode);
    }
}
