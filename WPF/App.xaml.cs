using BlackWindow.Configuration;
using BlackWindow.RabbitMQ.Core;
using BlackWindow.RabbitMQ.Core.Implementations;
using BlackWindow.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Unity;

namespace BlackWindow;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
        => Container.Resolve<BlackWindowView>();

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterSingleton<IConsumer, Consumer>()
            .RegisterSingleton<ISettings, Settings>();        
    }
}
