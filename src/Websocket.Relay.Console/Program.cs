using CommandLine;
using System.Threading.Tasks;

namespace Websocket.Relay.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            RunMode runMode = RunMode.Live;
            bool quit = true;
            int port = 0;

            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(x =>
                {
                    runMode = x.RunMode;
                    port = x.Port;
                    quit = false;
                });

            if (quit)
            {
                return;
            }

            var relay = Relay.Load(new Logger());
            await relay.Run(port, runMode);

            System.Console.WriteLine("Press any key to stop...");
            System.Console.ReadKey();
        }
    }

    public class Options
    {
        [Option('p', "port", Required = false, HelpText = "Specifies the port the web server will be run on. The default port is 7890")]
        public int Port { get; set; }

        [Option('t', "test", Required = false, HelpText = "Determines whether the relay should be run in test mode. Test mode will listen for the first client to connect and then send a pulse for 10 seconds to before shutting down")]
        public bool IsTestMode { get; set; }

        public RunMode RunMode
        {
            get
            {
                return IsTestMode ? RunMode.Test : RunMode.Live;
            }
        }

        public Options()
        {
            Port = 7890;
        }
    }
}
