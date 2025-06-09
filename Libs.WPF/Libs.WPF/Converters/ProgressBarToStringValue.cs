using System;
using System.Globalization;
using System.Windows.Data;

namespace Libs.WPF.Converters
{
    public class ProgressBarToStringValue : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double value)
            {
                return $"{value:0.0}%";
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}