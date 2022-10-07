using Prism.Commands;
using Prism.Mvvm;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using WPF.Models;

namespace WPF.ViewModels
{
    public class BlackWindowViewModel:BindableBase
    {
        public BlackWindowModel Model { get; }
        public ICommand DeleteCommand { get; }

        public BlackWindowViewModel(BlackWindowModel model)
        {
            Model = model;
            DeleteCommand = new DelegateCommand<object>(Model.DeleteText);
        }
    }
}
