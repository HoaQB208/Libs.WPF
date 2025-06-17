using System.Globalization;
using System.Windows.Data;
using System;

namespace Libs.WPF.Converters
{
    public class BoolInversion : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool vl) return !vl;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}