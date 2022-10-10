using BlackWindow.RabbitMQ.Core.Data;
using EasyNetQ;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BlackWindow.RabbitMQ.Core.Implementations;

public class Consumer : IConsumer
{
    protected IBus Bus { get; set; }

    public IObservable<string> MessagesObs { get; }      

    public string SubscriptionId { get; set; }

    public Consumer()
    {
#if DEBUG
        #region TODO Удалить когда появится доступ к шине
        MessagesObs = Observable
            .Interval(TimeSpan.FromSeconds(3))
            .Take(10)
            .Select(x => x % 2 == 1 ? ImageSamples.ImageJpg : ImageSamples.ImagePng);
        #endregion
#else
        SubscriptionId = "my_subscription_id";
        Bus = RabbitHutch.CreateBus("host=localhost");

        MessagesObs = Observable
            .Create<string>(observer => Bus.PubSub.SubscribeAsync<string>(SubscriptionId, observer.OnNext))
            .Publish()
            .AutoConnect(0);
#endif
    }
}
