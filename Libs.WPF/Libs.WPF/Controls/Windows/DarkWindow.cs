using Libs.WPF.Animations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Button = System.Windows.Controls.Button;

namespace Libs.WPF.Controls.Windows
{
    public class DarkWindow : Window
    {
        public DarkWindow(Window owner = null!)
        {
            Style = (Style)TryFindResource("DarkWindowStyle");

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
            RegisterComponents();
        }

        public bool IsHideAllControlBoxs { get; set; } = false;
        public bool IsHideMinimizeButton { get; set; } = false;
        public bool IsHideMaximizeButton { get; set; } = false;
        public bool IsHideCloseButton { get; set; } = false;
        public bool IsShowSnapshotButton { get; set; } = false;
        public bool IsShowSmallWindowButton { get; set; } = false;
        public bool IsHideTitleBar { get; set; } = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var topMost = this.Topmost;
            this.Topmost = true;

            // Giữ Topmost trong khi Window xuất hiện, tránh lỗi click đúp
            Task.Run(async () =>
            {
                await Task.Delay(500);
                System.Windows.Application.Current?.Dispatcher.Invoke(() =>
                {
                    this.Topmost = topMost;
                });
            });

            // Điều chỉnh nếu Width/Height có vượt quá kích thước màn hình
            WindowUtils.FitToScreenSize(this);

            // Hiệu ứng
            LoadedAnimation.WidthAndOpacity(this);

            if (IsHideTitleBar) TitleBar.Visibility = Visibility.Collapsed;

            if (IsHideAllControlBoxs) HideAllControlBoxs();
            else
            {
                if (IsHideMinimizeButton) HideMinimizeButton();
                if (IsHideMaximizeButton) HideMaximizeButton();
                if (IsHideCloseButton) HideCloseButton();
            }

            if (IsShowSnapshotButton && BtnSnapshot != null) BtnSnapshot.Visibility = Visibility.Visible;
            if (IsShowSmallWindowButton && BtnSmallWindow != null) BtnSmallWindow.Visibility = Visibility.Visible;
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            WindowUtils.SetResizeableCursor(this, e.GetPosition(this));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowUtils.ResizeWindow(this, Mouse.GetPosition(this));
        }

        private Button BtnSmallWindow = null!;
        private Button BtnSnapshot = null!;
        private Button BtnMinimize = null!;
        private Button BtnMaximize = null!;
        private Button BtnClose = null!;
        private Grid TitleBar = null!;
        private void RegisterComponents()
        {
            BtnSmallWindow = (Button)GetTemplateChild("BtnSmallWindow");
            if (BtnSmallWindow != null) BtnSmallWindow.Click += BtnSmallWindow_Click;

            BtnSnapshot = (Button)GetTemplateChild("BtnSnapshot");
            if (BtnSnapshot != null) BtnSnapshot.Click += BtnSnapshot_Click;

            BtnMinimize = (Button)GetTemplateChild("BtnMinimize");
            if (BtnMinimize != null) BtnMinimize.Click += BtnMinimize_Click;

            BtnMaximize = (Button)GetTemplateChild("BtnMaximize");
            if (BtnMaximize != null) BtnMaximize.Click += BtnMaximize_Click;

            BtnClose = (Button)GetTemplateChild("BtnExit");
            if (BtnClose != null) BtnClose.Click += BtnExit_Click;

            TitleBar = (Grid)GetTemplateChild("TitleBar");
        }

        bool isSmall = false;
        double normalWidth = 0;
        double normalHeight = 0;
        public double SmallWidth { get; set; }
        public double SmallHeight { get; set; }
        public void BtnSmallWindow_Click(object sender, RoutedEventArgs e)
        {
            isSmall = !isSmall;
            if (isSmall)
            {
                normalWidth = this.Width;
                normalHeight = this.Height;
                this.Width = SmallWidth;
                this.Height = SmallHeight;
            }
            else // Khôi phục lại ban đầu
            {
                this.Width = normalWidth;
                this.Height = normalHeight;
            }
        }

        public async void BtnSnapshot_Click(object sender, RoutedEventArgs e)
        {
            BtnSnapshot.Visibility = Visibility.Hidden;
            ToolTipService.SetIsEnabled(BtnSnapshot, false); // Ẩn tooltip
            await Task.Delay(10);
            try { WindowsSreenshot.ToClipbroad(Process.GetCurrentProcess().MainWindowHandle, 8, 8, 8, 8); } catch { }
            await Task.Delay(10);
            ToolTipService.SetIsEnabled(BtnSnapshot, true);
            BtnSnapshot.Visibility = Visibility.Visible;
        }

        public async void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            await MinimizeAnimation.OpacityAndMoveDown(this);
            this.WindowState = WindowState.Minimized;
            MinimizeAnimation.OpacityAndMoveDown_Restore(this);
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public void HideMinimizeButton()
        {
            if (BtnMinimize != null) BtnMinimize.Visibility = Visibility.Collapsed;
        }
        public void HideMaximizeButton()
        {
            if (BtnMaximize != null) BtnMaximize.Visibility = Visibility.Collapsed;
        }
        public void HideCloseButton()
        {
            if (BtnClose != null) BtnClose.Visibility = Visibility.Collapsed;
        }
        public void HideAllControlBoxs()
        {
            HideMinimizeButton();
            HideMaximizeButton();
            HideCloseButton();
        }
    }
}