using System.Threading.Tasks;

namespace Fadecandy.Relay.Console
{
    public class Logger : ILogger
    {
        public async Task Info(string message)
        {
            System.Console.WriteLine(message);
            await Task.Yield();
        }
    }
}
