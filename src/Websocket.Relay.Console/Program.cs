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

            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(x =>
                {
                    runMode = x.RunMode;
                    quit = false;
                });

            if (quit)
            {
                return;
            }

            var relay = Relay.Load(new Logger());
            await relay.Run(runMode);
        }
    }

    public class Options
    {
        [Option('t', "test", Required = false, HelpText = "Determines whether the relay should be run in test mode. Test mode will listen for the first client to connect and then send a pulse for 10 seconds to before shutting down")]
        public bool IsTestMode { get; set; }

        public RunMode RunMode
        {
            get
            {
                return IsTestMode ? RunMode.Test : RunMode.Live;
            }
        }
    }
}
