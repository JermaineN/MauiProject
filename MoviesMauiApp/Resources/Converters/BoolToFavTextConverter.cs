using System.Globalization;

namespace MoviesMauiApp.Resources.Converters;

public class BoolToFavTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "Remove Favourite" : "Add to Favourites";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
