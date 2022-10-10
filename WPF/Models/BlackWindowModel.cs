using BlackWindow.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using BlackWindow.RabbitMQ.Core;

namespace WPF.Models;

public class BlackWindowModel : ReactiveObject
{
    private int _amount = 1;
    
    public int DisplayLimit { get; init; } = 15;
    public ObservableCollection<ImageModel> PicsCollection { get; init; } = new ObservableCollection<ImageModel>();
    public extern bool IsNothing { [ObservableAsProperty] get; }


    public BlackWindowModel(IConsumer consumer)
    {
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
        var imageModel = new ImageModel(base64Image, (_amount++).ToString());
        PicsCollection.Add(imageModel); 
            
        Observable.Return(imageModel)
            .Delay(TimeSpan.FromSeconds(DisplayLimit))  
            .ObserveOnDispatcher()
            .Subscribe(DeleteImage);
    }

    public void DeleteImage(ImageModel m)
    {
        PicsCollection.Remove(m);
    }
}