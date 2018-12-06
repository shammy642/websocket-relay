using System.Threading.Tasks;

namespace Fadecandy.Relay
{
    public interface ILogger
    {
        Task Info(string message);
    }
}
