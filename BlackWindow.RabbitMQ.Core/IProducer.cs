namespace BlackWindow.RabbitMQ.Core;

public interface IProducer
{
    Task Publish(string text);
}
