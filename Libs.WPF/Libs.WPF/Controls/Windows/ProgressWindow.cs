using Libs.WPF.Animations;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Libs.WPF.Controls.Windows
{
    public class ProgressWindow : Window
    {
        public ProgressWindow(Window owner = null, string title = "Progress")
        {
            this.Title = title;

            try
            {
                ResourceDictionary dict = new ResourceDictionary() { Source = new Uri("/Libs.WPF;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute) };
                this.Resources.MergedDictionaries.Add(dict);
            }
            catch (Exception ex) { MessageBox.Show("Failed to load theme: " + ex.Message); }
            Style = (Style)TryFindResource("ProgressStyle");

            if (owner is null) WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.Owner = owner;
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            // Gắn sự kiện
            Loaded += Window_Loaded;
            MouseMove += Window_MouseMove;
            MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var topMost = this.Topmost;
            this.Topmost = true;

            // Điều chỉnh nếu Width/Height có vượt quá kích thước màn hình
            WindowUtils.FitToScreenSize(this);
            // Hiệu ứng
            LoadedAnimation.Opacity(this, 0.1);

            // Giữ Topmost trong khi Window xuất hiện, tránh lỗi click đúp
            Task.Run(async () =>
            {
                await Task.Delay(300);
                Application.Current?.Dispatcher.Invoke(() =>
                {
                    this.Topmost = topMost;
                });
            });
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            WindowUtils.SetResizeableCursor(this, e.GetPosition(this));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowUtils.ResizeWindow(this, Mouse.GetPosition(this));
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Escape) this.Close();
        }
    }
}
