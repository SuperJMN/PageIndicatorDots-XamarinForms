using System;
using System.Globalization;
using Xamarin.Forms;

namespace PageIndicatorDots.Controls
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return ColorForTrue;
            }

            return ColorForFalse;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Color ColorForTrue { get; set; } = Color.Red;
        public Color ColorForFalse { get; set; } = Color.Black;
    }
}