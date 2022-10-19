using EasyNetQ;
using EasyNetQ.Topology;

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
        var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest").Advanced;
        var queueName = "QueueTest";
        var queue = bus.QueueDeclare(queueName);
        var channel = bus.ExchangeDeclare("TestestExc", ExchangeType.Fanout);
        bus.Bind(channel, queue, "");
        return bus.PublishAsync(channel, "", false, new MessageProperties(), System.Text.Encoding.UTF8.GetBytes(text));
    }
}
