using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Threading.Tasks;
using System;
using Libs.WPF.Animations;

namespace Libs.WPF.Controls.Windows
{
    public class DialogWindow : Window
    {
        public string ShadowColor { get; private set; }
        string _message;
        bool _isShortMsg;

        public DialogWindow(object message, DialogType dialogType = DialogType.Information, bool isShortMsg = true, Window owner = null)
        {
            try
            {
                ResourceDictionary dict = new ResourceDictionary() { Source = new Uri("/Libs.WPF;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute) };
                this.Resources.MergedDictionaries.Add(dict);
            }
            catch (Exception ex) { MessageBox.Show("Failed to load theme: " + ex.Message); }
            Style = (Style)TryFindResource("DialogStyle");

            if (owner is null) WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.Owner = owner;
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            this.Title = dialogType.ToString();
            _message = message is null ? "null" : message.ToString();
            _isShortMsg = isShortMsg;
            switch (dialogType)
            {
                case DialogType.Information:
                    ShadowColor = "white";
                    break;
                case DialogType.Warning:
                    ShadowColor = "orange";
                    break;
                case DialogType.Error:
                    ShadowColor = "#FF8080";
                    break;
            }

            // Gắn sự kiện
            Loaded += Window_Loaded;
            MouseMove += Window_MouseMove;
            MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            RegisterComponents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var topMost = this.Topmost;
            this.Topmost = true;

            if (!_isShortMsg)
            {
                TbMessage.VerticalContentAlignment = VerticalAlignment.Top;
                TbMessage.HorizontalContentAlignment = HorizontalAlignment.Left;
            }

            // Điều chỉnh nếu Width/Height có vượt quá kích thước màn hình
            WindowUtils.FitToScreenSize(this);
            // Hiệu ứng
            LoadedAnimation.Opacity(this, 0.1);

            // Giữ Topmost trong khi Window xuất hiện, tránh lỗi click đúp
            Task.Run(async () =>
            {
                await Task.Delay(500);
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

        private Button BtnClose;
        private TextBox TbMessage;
        private void RegisterComponents()
        {
            BtnClose = (Button)GetTemplateChild("BtnExit");
            if (BtnClose != null) BtnClose.Click += BtnExit_Click;

            TbMessage = (TextBox)GetTemplateChild("TbMessage");
            if (TbMessage != null) TbMessage.Text = _message;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Escape) this.Close();
        }
    }

    public enum DialogType
    {
        Information,
        Warning,
        Error,
    }
}