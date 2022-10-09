using BlackWindow.Converters;
using System.Windows;

namespace WPF.Converters;

public class VisibilityConverter : BooleanConverter<Visibility, VisibilityConverter>
{
    public VisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
    {
    }
}

public class VisibilityReverseConverter : BooleanConverter<Visibility, VisibilityReverseConverter>
{
    public VisibilityReverseConverter() : base(Visibility.Collapsed, Visibility.Visible)
    {
    }
}