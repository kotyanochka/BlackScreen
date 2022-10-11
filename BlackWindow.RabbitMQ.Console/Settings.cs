using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.RabbitMQ.Console
{
    internal class Settings : ISettings
    {
        public string ConnectionString => "TODO из файла";

        public string SubscriptionId => "TODO из файла";

        public int ShowTime => 15;
    }
}
