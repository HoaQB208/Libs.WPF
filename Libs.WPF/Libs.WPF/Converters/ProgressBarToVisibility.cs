using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Libs.WPF.Converters
{
    public class ProgressBarToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                if (values[1] is ProgressBar progressBar)
                {
                    return progressBar.Value >= 100 ? Visibility.Hidden : Visibility.Visible;
                }
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}