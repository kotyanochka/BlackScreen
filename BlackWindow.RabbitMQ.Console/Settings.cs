using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.RabbitMQ.Console;

internal class Settings : ISettings
{
    public string ConnectionString => "host=localhost;virtualHost=/;username=guest;password=guest";

    public string QueueName => "BlackScreen";

    public int ShowTime => 15;

    public string ExchangeName => "TestExchange";
}
