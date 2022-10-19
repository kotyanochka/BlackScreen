using BlackWindow.RabbitMQ.Core.Data;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Reactive.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace BlackWindow.RabbitMQ.Core.Implementations;

public class Consumer : IConsumer
{
    protected IAdvancedBus Bus { get; init; }

    public string SubscriptionId { get; init; }

    public string ConnectionString { get; init; }

    public IExchange Exchange { get; init; }

    public IQueue Queue { get; init; }

    public IObservable<string> MessagesObs { get; }

    public Consumer(ISettings settings)
    {
#if DEBUG
        ConnectionString = settings.ConnectionString;
        SubscriptionId = settings.SubscriptionId;
        var rabbitBus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest");
        Bus = rabbitBus.Advanced;
        Exchange = Bus.ExchangeDeclare("TestestExc", EasyNetQ.Topology.ExchangeType.Fanout);
        Queue = Bus.QueueDeclare("QueueTest");
        Bus.Bind(Exchange, Queue, "");
        MessagesObs = Observable
            .Create<string>(observer => Bus.Consume())
            .Publish()
            .AutoConnect(0);
#endif
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

