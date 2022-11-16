using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlackWindow.Converters;

public abstract class ConverterBase<T> : MarkupExtension, IValueConverter
     where T : class, new()
{
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => new NotImplementedException();

    #region MarkupExtension members

    public override object ProvideValue(IServiceProvider serviceProvider) => _converter;

    private static readonly T _converter = new();

    #endregion
}
