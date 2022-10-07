using Prism.Mvvm;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using WPF.Services;

namespace WPF.Models
{
    public class BlackWindowModel:ReactiveObject //: BindableBase
    {
        [Reactive] public string Text { get; set; } = "Пусто.";
        [Reactive] public ObservableCollection<string> Texts { get; set; } = new ObservableCollection<string>() { "sdasd", "asdasdas", "dadadad" };
        [Reactive] public ObservableCollection<ImageModel> PicsCollection { get; set; }
        [Reactive] public bool IsNothing { get; private set; }

        private readonly IBlackWindowConsumer _consumer;

        public BlackWindowModel(IBlackWindowConsumer consumer)
        {
            PicsCollection = new ObservableCollection<ImageModel>();
            PicsCollection.CollectionChanged += TimersOnCollectionChanged;
            _consumer = consumer;
            //_consumer.Messages.Subscribe(s => App.Current.Dispatcher.InvokeAsync(() => Texts.Add(s)));
            _consumer.Messages.Subscribe(s => App.Current.Dispatcher.InvokeAsync(() => PicsCollection.Add(new ImageModel(s, TimeSpan.FromSeconds(5)))));
            //_consumer.Messages.Subscribe(x=> IsNothing = string.IsNullOrWhiteSpace(x)); 
            if (PicsCollection.Count == 0) IsNothing = true;
            foreach(var image in PicsCollection)
            {
                if (PicsCollection.Count == 1)
                {
                    PicsCollection[0]._width = 1920;
                    PicsCollection[0]._height = 1080;
                }
            }
        }

        public void DeleteText(object value)
        {
            if (value is string str && Texts.Contains(str))
            {
                Texts.Remove(str);
            }
        }

        //private void picsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ImageModel p = (ImageModel)picsList.SelectedItem;
        //    PicsCollection.Remove(p);
        //}

        private void TimersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ImageModel newItem in e.NewItems)
                {
                    newItem.Ended += DeleteOldItemsEvent;
                }
            }

            if (e.OldItems != null)
            {
                foreach (ImageModel newItem in e.OldItems)
                {
                    newItem.Ended -= DeleteOldItemsEvent;
                }
            }
        }

        private void DeleteOldItemsEvent(ImageModel obj) => PicsCollection.Remove(obj);
    }

    public class ImageModel:ReactiveObject
    {
        public ImageModel(string name, TimeSpan time)
        {
            PicPath = name;
            Time = time;
            End = DateTime.Now.Add(Time);
            StartTimer();
        }

        public string PicPath { get; set; }
        public int _width { get; set; } = 500;
        public int _height { get; set; } = 500;
        public TimeSpan Time { get; set; }
        public DateTime End { get; set; }
        private TimeSpan _timeLeft;
        public TimeSpan TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
            }
        }
        public event Action<ImageModel> Ended;
        private DispatcherTimer _deleteTimer;
        private DispatcherTimer _updateTimer;

        private void StartTimer()
        {
            _deleteTimer = new DispatcherTimer();
            _updateTimer = new DispatcherTimer();

            _deleteTimer.Interval = End - DateTime.Now;
            _updateTimer.Interval = TimeSpan.FromSeconds(1);

            _deleteTimer.Tick += DeleteOnTick;
            _updateTimer.Tick += UpdateOnTick;

            _deleteTimer.Start();
            _updateTimer.Start();
        }

        private void UpdateOnTick(object sender, EventArgs e)
        {
            TimeLeft = End - DateTime.Now;
        }

        private void DeleteOnTick(object sender, EventArgs e)
        {
            _deleteTimer.Stop();
            _updateTimer.Stop();
            Ended?.Invoke(this);
        }
    }
}
