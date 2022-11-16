using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace BlackWindow.Converters;
//Конвертер из base64 в bitmap
public class Base64ToBitmapConverter : ConverterBase<Base64ToBitmapConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string base64ImageString) return null;
        
        byte[] imageBytes = System.Convert.FromBase64String(base64ImageString);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        var data = Image.FromStream(ms, true);
        BitmapImage bitmap = new();
        bitmap.BeginInit();
        MemoryStream memoryStream = new();
        data.Save(memoryStream, ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);
        bitmap.StreamSource = memoryStream;
        bitmap.EndInit();
        return bitmap;
    }
}
