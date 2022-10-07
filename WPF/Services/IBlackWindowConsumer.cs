using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace WPF.Services
{
    public interface IBlackWindowConsumer
    {
        IObservable<string> Messages { get; }
    }

}
