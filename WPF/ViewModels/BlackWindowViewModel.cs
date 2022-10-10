using BlackWindow.Models;
using Prism.Commands;
using ReactiveUI;
using System.Windows.Input;
using WPF.Models;

namespace WPF.ViewModels;

public class BlackWindowViewModel : ReactiveObject
{
    public BlackWindowModel Model { get; }

    public BlackWindowViewModel(BlackWindowModel model)
    {
        Model = model;
        DeleteCommand = ReactiveCommand.Create<ImageModel>(Model.DeleteImage);
    }

    public ICommand DeleteCommand { get; }   
}
