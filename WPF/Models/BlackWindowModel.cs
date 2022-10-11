using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.Models;

public class BlackWindowModel : ReactiveObject
{
    public int DisplayLimit { get; init; }
    public ObservableCollection<ImageModel> PicsCollection { get; init; } = new ObservableCollection<ImageModel>();
    public extern bool IsNothing { [ObservableAsProperty] get; }


    public BlackWindowModel(IConsumer consumer, ISettings settings)
    {
        DisplayLimit = settings.ShowTime;

        consumer.MessagesObs
            .ObserveOnDispatcher()
            .Subscribe(AddImage);

        PicsCollection
            .WhenAnyValue(x => x.Count)     
            .Select(x=>x==0)
            .ToPropertyEx(this, x => x.IsNothing);
    }

    private void AddImage(string base64Image)
    {
        var imageModel = new ImageModel(base64Image);        
            
        Observable.Return(imageModel)
            .Delay(TimeSpan.FromSeconds(DisplayLimit))  
            .ObserveOnDispatcher()
            .Subscribe(DeleteImage);

        var timeoutObs = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Take(DisplayLimit - 1)
            .Select(x => (DisplayLimit - 1 - x).ToString());

        Observable
            .Return(DisplayLimit.ToString())
            .Merge(timeoutObs)
            .ToPropertyEx(imageModel, x => x.SecondsLeft);

        PicsCollection.Add(imageModel);
    }

    public void DeleteImage(ImageModel m)
    {
        PicsCollection.Remove(m);
    }
}