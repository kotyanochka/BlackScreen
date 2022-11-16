using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlackWindow.Models;

//Модель самого изображения
public class ImageModel : ReactiveObject
{
    //Строка в base64
    public string Base64String { get; init; }
    
    //Оставшееся количество секунд
    public extern string SecondsLeft { [ObservableAsProperty] get; }
    
    //Конструктор
    public ImageModel(string base64ImageString) 
    {
        Base64String = base64ImageString;       
    }    
}
        