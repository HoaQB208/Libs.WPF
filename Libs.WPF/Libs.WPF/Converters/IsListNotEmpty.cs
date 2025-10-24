using System.Globalization;
using System.Windows.Data;
using System;
using System.Linq;

namespace Libs.WPF.Converters
{
    public class IsListNotEmpty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is System.Collections.IEnumerable list && list.Cast<object>().Any();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}