using BlackWindow.RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class BlackWindowConsumer : IBlackWindowConsumer
    {
        public IObservable<string> Messages { get; }
        public IConsumer Consumer { get; }

        private ISubject<string> _txt;

        public BlackWindowConsumer(IConsumer consumer)
        {
           
            Consumer = consumer;
            // НАДО ИСПРАВИТЬ Consumer.Subscribe. 
          //Messages = Observable.FromAsync<string>(() => Consumer.Subscribe);

            #region УДАЛИТЬ ПОТОМ
            _txt = new ReplaySubject<string>();           
            Messages = _txt;
            //ToDo подключиться к RabbitMQ
            Task.Run(async () =>
            {//Тут были картинки, а теперь тут комментарий
                /*_txt.OnNext("Imagess/4.jpg");
                await Task.Delay(2000);
                _txt.OnNext("Imagess/1.jpg");
                _txt.OnNext("Imagess/3.jpg");
                await Task.Delay(2000);
                _txt.OnNext("Imagess/5.jpg");
                _txt.OnNext("Imagess/6.jpg");*/
                //_txt.OnNext("КАК ЭТО РАБОТАЕТ?!");
                //await Task.Delay(2000);
                //_txt.OnNext("Ох, вот оно что");
                //await Task.Delay(1000);
                //_txt.OnNext("А, нет, показалось.");
                //await Task.Delay(2000);
                //_txt.OnNext("Конец.");
                //await Task.Delay(2000);
                //_txt.OnNext("");
            });
            #endregion
        }
    }
}
