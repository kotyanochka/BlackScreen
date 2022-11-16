using BlackWindow.RabbitMQ.Core.Data;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Reactive.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlackWindow.RabbitMQ.Core.Implementations;
//реализация consumer'a
public class Consumer : IConsumer
{
    protected IAdvancedBus Bus { get; init; }

    public IExchange Exchange { get; init; }

    public IQueue Queue { get; init; }

    public IObservable<string> MessagesObs { get; }

    public Consumer(ISettings settings)
    {
        var rabbitBus = RabbitHutch.CreateBus(settings.ConnectionString); //Для подключения к RabbitMQ
        Bus = rabbitBus.Advanced; //Для расширения API с IAdvancedBus
        Exchange = Bus.ExchangeDeclare(settings.ExchangeName, EasyNetQ.Topology.ExchangeType.Fanout); // объевляем обмен
        Queue = Bus.QueueDeclare(settings.QueueName); // объявляем очередь
        Bus.Bind(Exchange, Queue, ""); // соединяемся с очередью
        //Каждый раз при приёме сообщения будет вызываться OnNext с полученной строкой
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

//    Factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" }; //binding к хосту
//    Connection = Factory.CreateConnection(); // создание соединения
//    Channel = Connection.CreateModel(); // создание канала обмена
//    Channel.ExchangeDeclare(exchange: "TestExc", type: ExchangeType.Fanout); // объевляем обмен c каналом Rabbit_Channel
//    _queueName = Channel.QueueDeclare().QueueName; // объявляем очередь
//    Channel.QueueBind(queue: _queueName, exchange: "TestExc", routingKey: ""); // соединяемся с очередью
//    var consumer = new EventingBasicConsumer(Channel); // обявляем consumer'a
//    consumer.Received += (model, ea) => // событие при приеме сообщения
//    {
//        var body = ea.Body.ToArray();
//        var text = System.Text.Encoding.UTF8.GetString(body);
//        //СЮДА НАДО ЧТО-ТО ВСТАВИТЬ, Хорошо хоть очередь создалась, обменник и канал - тоже.
//        Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); // подтверждаем прием сообщения
//    };
//    Channel.BasicConsume(queue: _queueName,
//                         autoAck: false,
//                         consumer: consumer);
//}

//Вот примерно к этому виду это всё надо привести
//Код отправки на стороне модели
//RabbitTopic.BasicPublish(exchange: Topic, routingKey: "", basicProperties: null, body: Encoding.UTF8.GetBytes(Convert.ToBase64String(Image.ToBytes())));

