using System.Globalization;
using System.Windows.Data;
using System.Windows;
using ProgressBar = System.Windows.Controls.ProgressBar;

namespace Libs.WPF.Converters
{
    public class ProgressBarToPosition : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                if (values[1] is ProgressBar progressBar)
                {
                    double progressValue = progressBar.Value;
                    double progressBarWidth = progressBar.ActualWidth;
                    double iconWidth = 55; // Đặt chiều rộng của PART_Icon tại đây
                    double margin = (progressValue / 100) * (progressBarWidth - iconWidth); // Tính toán vị trí ngang của PART_Icon dựa trên giá trị của ProgressBar
                    return new Thickness(margin, 0, 0, 0); // Trả về một đối tượng Thickness để đặt vị trí ngang cho PART_Icon
                }
            }
            return new Thickness(0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}