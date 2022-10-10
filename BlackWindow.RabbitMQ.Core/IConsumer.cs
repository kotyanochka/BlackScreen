namespace BlackWindow.RabbitMQ.Core;

public interface IConsumer 
{
    IObservable<string> MessagesObs { get; }
}
