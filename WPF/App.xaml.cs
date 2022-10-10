using BlackWindow.RabbitMQ.Core;
using BlackWindow.RabbitMQ.Core.Implementations;
using Prism.Ioc;
using System.Windows;
using Unity;
using WPF.Views;

namespace WPF;

public partial class App
{
    protected override Window CreateShell()
        => Container.Resolve<BlackWindowView>();

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterSingleton<IConsumer, Consumer>();        
    }
}
