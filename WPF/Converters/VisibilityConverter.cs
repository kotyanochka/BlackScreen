using System;
using System.Globalization;
using System.Windows;

namespace WPF.Converters
{
    /// <summary>
    /// Boolean to Visibility converter
    /// </summary>
    public class VisibilityConverter : ConverterBase<VisibilityConverter>
    {
        /// <summary>
        /// Direct
        /// </summary>
        /// <param name="value"><see cref="bool"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter != null && Equals(parameter, "Reverse"))
                {
                    if (value == null) return Visibility.Collapsed;
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                }

                if (value == null) return Visibility.Visible;
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            catch
            {
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Back
        /// </summary>
        /// <param name="value"><see cref="bool"/></param>  
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
    }

}
