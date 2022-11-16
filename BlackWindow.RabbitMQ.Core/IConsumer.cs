namespace BlackWindow.RabbitMQ.Core;
//Интерфейс для Consumer'a
public interface IConsumer 
{
    IObservable<string> MessagesObs { get; }
}
