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

public class BlackWindowModel : ReactiveObject //: BindableBase
{
    private readonly IBlackWindowConsumer _consumer;

    [Reactive] public ObservableCollection<ImageModel> PicsCollection { get; set; }
    [Reactive] public bool IsNothing { get; private set; }        

    public BlackWindowModel(IBlackWindowConsumer consumer)
    {
        PicsCollection = new ObservableCollection<ImageModel>();       

        _consumer = consumer;
       
        _consumer.Messages
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
        var imageModel = new ImageModel(base64Image);

        PicsCollection.Add(imageModel);
            
        Observable
            .Timer(DateTimeOffset.Now.AddSeconds(15))
            .ObserveOnDispatcher()
            .Subscribe(_ => DeleteImage(imageModel))
            .ToDisposable();
    }

    public void DeleteImage(object value)
    {
        if (value is ImageModel m && PicsCollection.Contains(m))
        {
            PicsCollection.Remove(m);
        }
    }
}