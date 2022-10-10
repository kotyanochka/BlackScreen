namespace BlackWindow.RabbitMQ.Core;

public interface ISettings
{
    string ConnectionString { get; }
    string SubscriptionId { get; }
    int ShowTime { get; }
}
