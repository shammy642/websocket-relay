using System;
using System.Threading.Tasks;

namespace Websocket.Relay.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var relay = Relay.Load(new ConsoleLogger(), RelayConfiguration.Default);
            await relay.Run(7890, RunMode.Test);

            System.Threading.Thread.Sleep(TimeSpan.FromHours(24));

            //System.Console.WriteLine("Press any key to quit");
            //System.Console.Read();
        }
    }

    public class ConsoleLogger : ILogger
    {
        public async Task Info(string message)
        {
            await Task.CompletedTask;
            System.Console.WriteLine(message);
        }
    }
}
