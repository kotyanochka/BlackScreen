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

//Вот примерно к этому виду это всё надо привести
//Код отправки на стороне модели
//RabbitTopic.BasicPublish(exchange: Topic, routingKey: "", basicProperties: null, body: Encoding.UTF8.GetBytes(Convert.ToBase64String(Image.ToBytes())));

/*#region RabbitMQ Worker
        /// <summary>
        /// Запуск RabbitMQ и прием сообщений от модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RabbitWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Factory = new ConnectionFactory() { HostName = Rabbit_Host, Port = Convert.ToInt32(Rabbit_Port), UserName = "CV_MODEL", Password = "YOUR_PWD", VirtualHost = "CV" }; //binding к хосту
            Connection = Factory.CreateConnection(); // создание соединения
            Channel = Connection.CreateModel(); // создание канала обмена
            try
            {
                Channel.ExchangeDeclare(exchange: Rabbit_Channel, type: ExchangeType.Fanout); // объевляем обмен c каналом Rabbit_Channel
                var queueName = Channel.QueueDeclare().QueueName; // объявляем очередь
                Channel.QueueBind(queue: queueName, exchange: Rabbit_Channel, routingKey: ""); // соединяемся с очередью
                var consumer = new EventingBasicConsumer(Channel); // обявляем consumer'a
                consumer.Received += (model, ea) => // событие при приеме сообщения
                {
                    var body = ea.Body.ToArray();
                    Rabbit_Message = Encoding.UTF8.GetString(body);
                    try
                    {
                        var decodedByteArray = Convert.FromBase64String(Rabbit_Message); // 
                        Received_Img = Cv2.ImDecode(decodedByteArray, LoadMode.Color);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteStringToLog(ex.ToString());
                    }

                    if (Received_Img.Cols>0)
                    {
                       using (var bitmap = Received_Img.Clone().ToBitmap())
                       {
                           var bitmap_image = new BitmapImage();
                           bitmap_image.BeginInit();
                           MemoryStream ms = new MemoryStream();
                           bitmap.Save(ms, ImageFormat.Bmp);
                           bitmap_image.StreamSource = ms;
                           bitmap_image.CacheOption = BitmapCacheOption.None;
                           bitmap_image.EndInit();
                           bitmap_image.Freeze();
                           DisplayedBitmap = bitmap_image;
                       }
                    }
                    Received_Img = null;
                    Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); // подтверждаем прием сообщения
                };
                Channel.BasicConsume(queue: queueName,
                                     autoAck: false,
                                     consumer: consumer);
            }
            catch (Exception ex)
            {
                Log.WriteStringToLog("Rabbit Thread Error - " + ex.ToString());
                Log.WriteStringToLog("Stack Trace - " + ex.StackTrace.ToString());
                Log.WriteStringToLog("Inner EX - " + ex.InnerException.ToString());
            }
        }
        #endregion 
*/