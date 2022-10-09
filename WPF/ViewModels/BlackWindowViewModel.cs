using Prism.Commands;
using ReactiveUI;
using System.Windows.Input;
using WPF.Models;

namespace WPF.ViewModels;

public class BlackWindowViewModel : ReactiveObject
{
    public BlackWindowModel Model { get; }
    public ICommand DeleteCommand { get; }

    public BlackWindowViewModel(BlackWindowModel model)
    {
        Model = model;
        DeleteCommand = new DelegateCommand<object>(Model.DeleteImage);
    }
}
