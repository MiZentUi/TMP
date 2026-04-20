using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace LR13_14.UI.ValueConverters;

internal class ServiceIdToImageConverter : IValueConverter
{
    private readonly static string placeholderPath = "avares://LR13_14/Resources/Images/placeholder.png";

    public static Bitmap Convert(int id)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var imagePath = Directory.GetFiles(path)
            .Where(f => Path.GetFileNameWithoutExtension(f).Equals(id.ToString()))
            .FirstOrDefault(placeholderPath);

        if (!imagePath.Equals(placeholderPath))
        {
            return new Bitmap(imagePath);
        }

        return new Bitmap(AssetLoader.Open(new Uri(placeholderPath)));
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int id)
        {
            return Convert(id);
        }

        return new Bitmap(AssetLoader.Open(new Uri(placeholderPath)));
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}