using BlackWindow.RabbitMQ.Core;
using BlackWindow.RabbitMQ.Core.Data;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class BlackWindowConsumer : IBlackWindowConsumer
    {
        public IObservable<string> Messages { get; }
        public IConsumer Consumer { get; }

        private readonly ISubject<string> _txt;

        public BlackWindowConsumer(IConsumer consumer)
        {
           
            Consumer = consumer;
            // НАДО ИСПРАВИТЬ Consumer.Subscribe. 
            //Messages = Observable.FromAsync<string>(() => Consumer.Subscribe);

            #region УДАЛИТЬ ПОТОМ            
             
            Messages = _txt = new ReplaySubject<string>();

            //ToDo подключиться к RabbitMQ
            Task.Run(async () =>
            {
                /*_txt.OnNext("Imagess/4.jpg");
                await Task.Delay(2000);
                _txt.OnNext("Imagess/1.jpg");
                _txt.OnNext("Imagess/3.jpg");
                await Task.Delay(2000);
                _txt.OnNext("Imagess/5.jpg");
                _txt.OnNext("Imagess/6.jpg");*/

                for (int i = 0; i < 10; i++)
                {
                    _txt.OnNext(ImageSamples.ImagePng);
                    await Task.Delay(5000);
                }              
            });
            #endregion
        }
    }
}
