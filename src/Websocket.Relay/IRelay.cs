using System.Threading.Tasks;

namespace Fadecandy.Relay
{
    public interface IRelay
    {
        Task Run(RunMode mode);
    }
}
