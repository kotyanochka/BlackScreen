using EasyNetQ;
using EasyNetQ.Topology;
using System.Reactive.Linq;


namespace BlackWindow.RabbitMQ.Core.Implementations;
public class Consumer : IConsumer
{
    protected IAdvancedBus Bus { get; init; }

    public IExchange Exchange { get; init; }

    public IQueue Queue { get; init; }

    public IObservable<string> MessagesObs { get; }

    public Consumer(ISettings settings)
    {
        var rabbitBus = RabbitHutch.CreateBus(settings.ConnectionString);
        Bus = rabbitBus.Advanced;
        Exchange = Bus.ExchangeDeclare(settings.ExchangeName, ExchangeType.Fanout);
        Queue = Bus.QueueDeclare(settings.QueueName); 
        Bus.Bind(Exchange, Queue, "");
        MessagesObs = Observable
            .Create<string>(observer => Bus.Consume(Queue, (body, properties, info) =>
            {
                string content = System.Text.Encoding.UTF8.GetString(body);
                observer.OnNext(content);
            }))
            .Publish()
            .AutoConnect(0);
    }
}

