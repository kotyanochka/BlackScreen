namespace BlackWindow.RabbitMQ.Core;
//Интерфейс для Producer'a
public interface IProducer
{
    Task Publish(string text);
}
