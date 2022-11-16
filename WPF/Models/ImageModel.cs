using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlackWindow.Models;

public class ImageModel : ReactiveObject
{
    /// <summary>
    /// Строка в base64
    /// </summary>
    public string Base64String { get; init; }
    /// <summary>
    /// Количество оставшихся секунд
    /// </summary>
    public extern string SecondsLeft { [ObservableAsProperty] get; }

    public ImageModel(string base64ImageString) 
    {
        Base64String = base64ImageString;       
    }    
}
        