using EasyNetQ;

namespace BlackWindow.RabbitMQ.Core.Implementations;

public class Producer : IProducer
{
    public string ConnectionString { get; init; }

    public Producer(ISettings settings)
    {
        ConnectionString = settings.ConnectionString;
    }
    public Task Publish(string text)
    {
        using var bus = RabbitHutch.CreateBus(ConnectionString);
        return bus.PubSub.PublishAsync(text);
    }
}
