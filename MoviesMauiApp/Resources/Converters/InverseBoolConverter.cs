using System.Globalization;

namespace MoviesMauiApp.Resources.Converters;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b) return !b;
        if (value is int i) return i == 0; // Treat 0 as false -> true (Empty list -> Show empty view)
        return false; 
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
