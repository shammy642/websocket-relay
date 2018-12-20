namespace Websocket.Relay
{
    public class RelayConfiguration
    {
        public static RelayConfiguration Default => new RelayConfiguration
        {
            MaxFramesPerSecond = 60
        };

        public double MaxFramesPerSecond { get; set; }
    }
}
