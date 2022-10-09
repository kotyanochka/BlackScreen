using System;
using System.Collections.Generic;

namespace BlackWindow.Extensions;

public static class ObservableExtensions
{
    private static readonly ICollection<IDisposable> _observers = new List<IDisposable>();

    public static IDisposable ToDisposable(this IDisposable disposable)
    {
        _observers.Add(disposable);
        return disposable;
    }
}
