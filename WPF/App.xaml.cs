using BlackWindow.RabbitMQ.Core;
using BlackWindow.RabbitMQ.Core.Implementations;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using WPF.Services;
using WPF.Views;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
            => Container.Resolve<BlackWindowView>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IBlackWindowConsumer, BlackWindowConsumer>()
                .RegisterSingleton<IConsumer, Consumer>();
            
        }
    }
}
