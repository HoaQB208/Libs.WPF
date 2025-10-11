using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;
using Cursor = System.Windows.Input.Cursor;
using Cursors = System.Windows.Input.Cursors;

namespace Libs.WPF.Controls.Windows
{
    public class WindowUtils
    {
        private const int resizeBorder = 12; // Khoảng cách từ các cạnh và góc để xem xét việc thay đổi kích thước

        public static void FitToScreenSize(Window window)
        {
            if (window.Height > SystemParameters.PrimaryScreenHeight || window.Width > SystemParameters.PrimaryScreenWidth)
            {
                if (window.Height > SystemParameters.PrimaryScreenHeight) window.Height = SystemParameters.PrimaryScreenHeight * 0.9;
                if (window.Width > SystemParameters.PrimaryScreenWidth) window.Width = SystemParameters.PrimaryScreenWidth * 0.95;
                // Chỉnh lại vị trí giữa màn hình
                window.Top = (SystemParameters.PrimaryScreenHeight - window.Height) / 2;
                window.Left = (SystemParameters.PrimaryScreenWidth - window.Width) / 2;
            }
        }

        public static void CloseOtherWindows(Window exceptWindow)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                foreach (Window w in Application.Current.Windows) if (w != exceptWindow) w.Close();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window w in Application.Current.Windows) if (w != exceptWindow) w.Close();
                });
            }
        }

        public static void CloseOtherWindows(string exceptTypeName)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w != null && w.GetType().Name != exceptTypeName) w.Close();
                }
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w != null && w.GetType().Name != exceptTypeName) w.Close();
                    }
                });
            }
        }

        public static Window FindWindow(string typeName)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w != null && w.GetType().Name == typeName) return w;
                }
            }
            else
            {
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w != null && w.GetType().Name == typeName) return w;
                    }
                    return null;
                });
            }
            return null;
        }


        public static bool RestoredWindows(Window wd)
        {
            if (wd != null && wd.IsVisible)
            {
                var topMost = wd.Topmost;
                wd.Topmost = true;
                if (wd.WindowState == WindowState.Minimized) wd.WindowState = WindowState.Normal;

                // Giữ Topmost trong khi Window xuất hiện, tránh lỗi click đúp
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        wd.Topmost = topMost;
                    });
                });
                return true;
            }
            return false;
        }


        /// <summary>
        /// Điều chỉnh kích thước Window
        /// </summary>
        /// <param name="cursorPoint">Vị trí của con trỏ chuột trong cửa sổ: e.GetPosition(this)</param>
        /// <returns></returns>
        public static void SetResizeableCursor(Window window, System.Windows.Point cursorPoint)
        {
            Cursor cursor = Cursors.Arrow; // Con trỏ mặc định
            // Kiểm tra nếu con trỏ chuột nằm ở các cạnh và góc của cửa sổ
            if (cursorPoint.X <= resizeBorder && cursorPoint.Y <= resizeBorder)
            {
                cursor = Cursors.SizeNWSE; // Góc trên bên trái
            }
            else if (cursorPoint.X >= window.ActualWidth - resizeBorder && cursorPoint.Y <= resizeBorder)
            {
                cursor = Cursors.SizeNESW; // Góc trên bên phải
            }
            else if (cursorPoint.X <= resizeBorder && cursorPoint.Y >= window.ActualHeight - resizeBorder)
            {
                cursor = Cursors.SizeNESW; // Góc dưới bên trái
            }
            else if (cursorPoint.X >= window.ActualWidth - resizeBorder && cursorPoint.Y >= window.ActualHeight - resizeBorder)
            {
                cursor = Cursors.SizeNWSE; // Góc dưới bên phải
            }
            else if (cursorPoint.X <= resizeBorder)
            {
                cursor = Cursors.SizeWE; // Cạnh trái
            }
            else if (cursorPoint.X >= window.ActualWidth - resizeBorder)
            {
                cursor = Cursors.SizeWE; // Cạnh phải
            }
            else if (cursorPoint.Y <= resizeBorder)
            {
                cursor = Cursors.SizeNS; // Cạnh trên
            }
            else if (cursorPoint.Y >= window.ActualHeight - resizeBorder)
            {
                cursor = Cursors.SizeNS; // Cạnh dưới
            }
            if (window.Cursor != cursor) window.Cursor = cursor;
        }



        // Thay đổi kích thước Window bằng cách đưa con trỏ chuột vào cạnh Window và kéo
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int HT_BOTTOMLEFT = 0x10;
        private const int HT_BOTTOMRIGHT = 0x11;
        private const int HT_LEFT = 0xA;
        private const int HT_RIGHT = 0xB;
        private const int HT_TOP = 0xC;
        private const int HT_BOTTOM = 0xF;
        private const int HT_TOPLEFT = 0xD;
        private const int HT_TOPRIGHT = 0xE;

        private static void ResizeWindow(Window window, int direction)
        {
            try
            {
                ReleaseCapture();
                SendMessage(new System.Windows.Interop.WindowInteropHelper(window).Handle, WM_NCLBUTTONDOWN, direction, 0);
            }
            catch { }
        }

        public static void ResizeWindow(Window window, System.Windows.Point cursorPoint)
        {
            double width = window.ActualWidth;
            double height = window.ActualHeight;

            if (cursorPoint.X <= resizeBorder && cursorPoint.Y <= resizeBorder)
            {
                ResizeWindow(window, HT_TOPLEFT);
            }
            else if (cursorPoint.X >= width - resizeBorder && cursorPoint.Y <= resizeBorder)
            {
                ResizeWindow(window, HT_TOPRIGHT);
            }
            else if (cursorPoint.X <= resizeBorder && cursorPoint.Y >= height - resizeBorder)
            {
                ResizeWindow(window, HT_BOTTOMLEFT);
            }
            else if (cursorPoint.X >= width - resizeBorder && cursorPoint.Y >= height - resizeBorder)
            {
                ResizeWindow(window, HT_BOTTOMRIGHT);
            }
            else if (cursorPoint.X <= resizeBorder)
            {
                ResizeWindow(window, HT_LEFT);
            }
            else if (cursorPoint.X >= width - resizeBorder)
            {
                ResizeWindow(window, HT_RIGHT);
            }
            else if (cursorPoint.Y <= resizeBorder)
            {
                ResizeWindow(window, HT_TOP);
            }
            else if (cursorPoint.Y >= height - resizeBorder)
            {
                ResizeWindow(window, HT_BOTTOM);
            }
            else
            {
                ResizeWindow(window, HT_CAPTION);
            }
        }
    }
}