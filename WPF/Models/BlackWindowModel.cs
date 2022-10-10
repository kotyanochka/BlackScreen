using BlackWindow.Models;
using BlackWindow.RabbitMQ.Core.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using WPF.Services;
using System.Reactive.Linq;
using BlackWindow.Extensions;

namespace WPF.Models;

public class BlackWindowModel : ReactiveObject
{
    private int _amount = 1;
    public int DisplayLimit { get; set; } = 15;
    public ObservableCollection<ImageModel> PicsCollection { get; set; } = new ObservableCollection<ImageModel>();
    [Reactive] public bool IsNothing { get; private set; }
    
    public BlackWindowModel(IBlackWindowConsumer consumer)
    {
        consumer.Messages
            .ObserveOnDispatcher()
            .Subscribe(Add);

        PicsCollection
            .WhenAnyValue(x => x.Count)     
            .Select(x=>x ==0)
            .Subscribe(x => IsNothing = x)
            .ToDisposable();
    }

    private void Add(string base64Image)
    {
        var imageModel = new ImageModel(base64Image)
        {
            Text = (_amount++).ToString()
        };

        PicsCollection.Add(imageModel);
            
        Observable
            .Timer(DateTimeOffset.Now.AddSeconds(DisplayLimit))
            .ObserveOnDispatcher()
            .Subscribe(_ => DeleteImage(imageModel));
    }

    public void DeleteImage(object value)
    {
        if (value is not ImageModel m || !PicsCollection.Contains(m)) return;

        PicsCollection.Remove(m);
    }
}