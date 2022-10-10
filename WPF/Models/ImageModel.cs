using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlackWindow.Models;

public class ImageModel : ReactiveObject
{
    public string Base64String { get; init; }

    public extern string SecondsLeft { [ObservableAsProperty] get; }

    public ImageModel(string base64ImageString) 
    {
        Base64String = base64ImageString;       
    }    
}
        