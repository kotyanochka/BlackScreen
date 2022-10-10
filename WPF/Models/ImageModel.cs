using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace BlackWindow.Models;

public class ImageModel
{
    public BitmapImage Bitmap { get; init; }

    public string Text { get; init; }

    public ImageModel(string base64ImageString, string text) 
    {
        byte[] imageBytes = Convert.FromBase64String(base64ImageString);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        var data = Image.FromStream(ms, true);
        BitmapImage bitmap = new();
        bitmap.BeginInit();
        MemoryStream memoryStream = new();
        data.Save(memoryStream, ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);
        bitmap.StreamSource = memoryStream;
        bitmap.EndInit();
        Bitmap =  bitmap;
        Text = text;
    }    
}
