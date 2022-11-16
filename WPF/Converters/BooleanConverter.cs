using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlackWindow.Converters;
public class BooleanConverter<T, TMarkup> : ConverterBase<TMarkup> where TMarkup : BooleanConverter<T, TMarkup>, new()
{
    public BooleanConverter(T trueValue, T falseValue)
    {
        TrueValue = trueValue;
        FalseValue = falseValue;
    }

    public T TrueValue { get; set; }

    public T FalseValue { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true ? TrueValue : FalseValue;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is T tValue && EqualityComparer<T>.Default.Equals(tValue, TrueValue);
    }
}