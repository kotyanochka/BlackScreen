using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.RabbitMQ.Console
{
    internal class Settings : ISettings
    {
        public string ConnectionString => "host=localhost;virtualHost=/;username=guest;password=guest";

        public string SubscriptionId => "BlackScreen";

        public int ShowTime => 15;
    }
}
