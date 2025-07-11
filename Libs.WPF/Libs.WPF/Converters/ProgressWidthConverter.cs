﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Libs.WPF.Converters
{
    public class ProgressWidthConverter : IMultiValueConverter
    {
        // values[0] = ActualWidth
        // values[1] = Value
        // values[2] = Maximum (optional)
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return 0;

            if (!(values[0] is double) || !(values[1] is double))
                return 0;

            double actualWidth = (double)values[0];
            double value = (double)values[1];


            double maximum = 100;
            if (values.Length > 2 && values[2] is double max)
                maximum = max;

            if (maximum <= 0)
                return 0;

            double ratio = Math.Max(0, Math.Min(1, value / maximum));
            return actualWidth * ratio;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}