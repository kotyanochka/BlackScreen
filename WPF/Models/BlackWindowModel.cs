using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.Models;
//Модель с обработкой коллекции картинок
public class BlackWindowModel : ReactiveObject
{
    //Время отображения
    public int DisplayLimit { get; init; }
    //Коллекция с картинками
    public ObservableCollection<ImageModel> PicsCollection { get; init; } = new ObservableCollection<ImageModel>();
    //Для проверки на пустоту
    public extern bool IsNothing { [ObservableAsProperty] get; }


    public BlackWindowModel(IConsumer consumer, ISettings settings)
    {
        //Время отображения берётся из настроек
        DisplayLimit = settings.ShowTime;
        
        //Добавление картинки, если она пришла, вызов AddImage
        consumer.MessagesObs
            .ObserveOnDispatcher()
            .Subscribe(AddImage);
        
        //Проверяются изменения в коллекции, если пустая - x.IsNothing 
        PicsCollection
            .WhenAnyValue(x => x.Count)     
            .Select(x=>x==0)
            .ToPropertyEx(this, x => x.IsNothing);
    }

    private void AddImage(string base64Image)
    {
        var imageModel = new ImageModel(base64Image);        
        
        //Жизнь изображения. Ставится задержка на DisplayLimit, после - удаляется            
        Observable.Return(imageModel)
            .Delay(TimeSpan.FromSeconds(DisplayLimit))  
            .ObserveOnDispatcher()
            .Subscribe(DeleteImage);

        //Наблюдатель для таймера
        var timeoutObs = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Take(DisplayLimit - 1)
            .Select(x => (DisplayLimit - 1 - x).ToString());

        //Изменение оставшегося времени
        Observable
            .Return(DisplayLimit.ToString())
            .Merge(timeoutObs)
            .ToPropertyEx(imageModel, x => x.SecondsLeft);
        
        //Добавление в коллекцию
        PicsCollection.Add(imageModel);
    }

    //Удаление картинки из коллекции
    public void DeleteImage(ImageModel m)
    {
        PicsCollection.Remove(m);
    }
}