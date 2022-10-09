using System;

namespace WPF.Services;

public interface IBlackWindowConsumer
{
    IObservable<string> Messages { get; }
}
