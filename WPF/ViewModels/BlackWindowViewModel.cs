using BlackWindow.Models;
using ReactiveUI;
using System.Windows.Input;

namespace BlackWindow.ViewModels;
//Модель представления
public class BlackWindowViewModel : ReactiveObject
{   
    //Модель
    public BlackWindowModel Model { get; }
    
    //Контсруктор
    public BlackWindowViewModel(BlackWindowModel model)
    {
        Model = model;
        DeleteCommand = ReactiveCommand.Create<ImageModel>(Model.DeleteImage);
    }
    
    //Команда для удаления изображения по нажатию
    public ICommand DeleteCommand { get; }   
}
