namespace BlackWindow.RabbitMQ.Core;

public interface ISettings
{
    string ConnectionString { get; }
    string QueueName { get; }
    string ExchangeName { get; }
    int ShowTime { get; }
}
