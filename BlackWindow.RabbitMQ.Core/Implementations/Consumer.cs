using BlackWindow.RabbitMQ.Core.Data;
using EasyNetQ;
using System.Reactive.Linq;

namespace BlackWindow.RabbitMQ.Core.Implementations;

public class Consumer : IConsumer
{
    protected IBus Bus { get; init; }

    public string SubscriptionId { get; init; }

    public string ConnectionString { get; init; }

    public IObservable<string> MessagesObs { get; } 

    public Consumer(ISettings settings)
    {
        ConnectionString = settings.ConnectionString;
        SubscriptionId = settings.SubscriptionId;
        Bus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest");

//#if DEBUG //TODO Удалить когда появится доступ к шине
//        MessagesObs = Observable
//            .Interval(TimeSpan.FromSeconds(3))
//            .Take(10)
//            .Select(x => x % 2 == 1 ? ImageSamples.ImageJpg : ImageSamples.ImagePng);
//#else
        MessagesObs = Observable
            .Create<string>(observer => Bus.PubSub.Subscribe<string>(SubscriptionId, observer.OnNext))
            .Publish()
            .AutoConnect(0);
//#endif
    }
}
