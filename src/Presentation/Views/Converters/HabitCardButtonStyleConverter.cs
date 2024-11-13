using System.Globalization;

namespace Presentation.Views.Converters
{
    public class HabitCardButtonStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCompleted)
            {
                if (parameter as string == "BackgroundColor")
                {
                    return isCompleted ? (Color)Application.Current!.Resources["Secondary"] : "Transparent";
                }
                if (parameter as string == "TextColor")
                {
                    return isCompleted ? (Color)Application.Current!.Resources["Primary"] : (Color)Application.Current!.Resources["Secondary"];
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}