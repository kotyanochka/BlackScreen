using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace BlackWindow.Models;

public class ImageModel : ReactiveObject
{
    /// <summary>
    /// Тут под любое изображение подходит, но есть проблемы привязки
    /// </summary>
    [Reactive] public Image Data { get; set; }

    /// <summary>
    /// Тут данные приводятся к BitmapImage, который легко привязывается
    /// </summary>
    [Reactive] public BitmapImage Bitmap { get; set; }

    [Reactive] public string Text { get; set; }

    public ImageModel(string base64ImageString)
    {
        byte[] imageBytes = Convert.FromBase64String(base64ImageString);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        Data = Image.FromStream(ms, true);

        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        MemoryStream memoryStream = new MemoryStream();
        Data.Save(memoryStream, ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);
        bitmap.StreamSource = memoryStream;
        bitmap.EndInit();
        Bitmap =  bitmap;
    }    
}
