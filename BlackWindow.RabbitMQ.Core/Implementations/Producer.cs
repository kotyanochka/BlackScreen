using EasyNetQ;
using EasyNetQ.Topology;

namespace BlackWindow.RabbitMQ.Core.Implementations;
public class Producer : IProducer
{
    public string ConnectionString { get; init; }
    public string QueueName { get; init; }
        
    public Producer(ISettings settings)
    {
        ConnectionString = settings.ConnectionString;
        QueueName = settings.QueueName;
    }

    public Task Publish(string text)
    {
        var bus = RabbitHutch.CreateBus(ConnectionString).Advanced;
        var queue = bus.QueueDeclare(QueueName);
        var channel = bus.ExchangeDeclare(QueueName, ExchangeType.Fanout);
        bus.Bind(channel, queue, "");
        return bus.PublishAsync(channel, "", false, new MessageProperties(), System.Text.Encoding.UTF8.GetBytes(text));
    }
}
