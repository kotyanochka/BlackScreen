using BlackWindow.RabbitMQ.Core.Data;
using EasyNetQ;
using System.Reactive.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlackWindow.RabbitMQ.Core.Implementations;

public class Consumer : IConsumer
{
    //protected IBus Bus { get; init; }

    //public string SubscriptionId { get; init; }

    //public string ConnectionString { get; init; }
    public ConnectionFactory Factory { get; init; }
    public IConnection Connection { get; init; }
    public IModel Channel { get; init; }
    public IObservable<string> MessagesObs { get; }

    public Consumer(ISettings settings)
    {
        #if DEBUG 
        Factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest"}; //binding к хосту
        Connection = Factory.CreateConnection(); // создание соединения
        Channel = Connection.CreateModel(); // создание канала обмена
        Channel.ExchangeDeclare(exchange: "TestExc", type: ExchangeType.Fanout); // объевляем обмен c каналом Rabbit_Channel
        var queueName = Channel.QueueDeclare().QueueName; // объявляем очередь
        Channel.QueueBind(queue: queueName, exchange: "TestExc", routingKey: ""); // соединяемся с очередью
        var consumer = new EventingBasicConsumer(Channel); // обявляем consumer'a
        consumer.Received += (model, ea) => // событие при приеме сообщения
        {
            //СЮДА НАДО ЧТО-ТО ВСТАВИТЬ, Хорошо хоть очередь создалась, обменник и канал - тоже.
                Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); // подтверждаем прием сообщения
        };
        Channel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);
        #else
        ConnectionString = settings.ConnectionString;
        SubscriptionId = settings.SubscriptionId;
        Bus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest");

        MessagesObs = Observable
            .Create<string>(observer => Bus.PubSub.Subscribe<string>(SubscriptionId, observer.OnNext))
            .Publish()
            .AutoConnect(0);
        #endif
    }
}

//Вот примерно к этому виду это всё надо привести
//Код отправки на стороне модели
//RabbitTopic.BasicPublish(exchange: Topic, routingKey: "", basicProperties: null, body: Encoding.UTF8.GetBytes(Convert.ToBase64String(Image.ToBytes())));