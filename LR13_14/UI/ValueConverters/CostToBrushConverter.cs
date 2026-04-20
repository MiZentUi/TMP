using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace LR13_14.UI.ValueConverters;

internal class CostToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double cost && cost < 30)
        {
            return new SolidColorBrush(Colors.Red);
        }
        return new SolidColorBrush(Colors.Black);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}