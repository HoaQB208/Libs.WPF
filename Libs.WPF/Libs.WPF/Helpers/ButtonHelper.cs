using System.Windows;
using System.Windows.Media;

namespace Libs.WPF.Helpers
{
    public static class ButtonHelper
    {
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.RegisterAttached("IconSize", typeof(double), typeof(ButtonHelper), new PropertyMetadata(16.0));
        public static void SetIconSize(UIElement element, double value) => element.SetValue(IconSizeProperty, value);
        public static double GetIconSize(UIElement element) => (double)element.GetValue(IconSizeProperty);


        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.RegisterAttached("TextColor", typeof(Brush), typeof(ButtonHelper), new PropertyMetadata(Brushes.Black));
        public static void SetTextColor(UIElement element, Brush value) => element.SetValue(TextColorProperty, value);
        public static Brush GetTextColor(UIElement element) => (Brush)element.GetValue(TextColorProperty);
    }
}
