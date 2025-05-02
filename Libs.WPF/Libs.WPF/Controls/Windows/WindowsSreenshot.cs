using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Libs.WPF.Controls.Windows
{
    class WindowsSreenshot
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int m_left;
            public int m_top;
            public int m_right;
            public int m_bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        public static void SaveToFile(IntPtr handle_, string pathSaveFile, int paddingLeft = 0, int paddingTop = 0, int paddingRight = 0, int paddingBot = 0)
        {
            RECT windowRect = new();
            GetWindowRect(handle_, ref windowRect);

            int width = windowRect.m_right - windowRect.m_left - paddingLeft - paddingRight;
            int height = windowRect.m_bottom - windowRect.m_top - paddingTop - paddingBot;
            Point topLeft = new(windowRect.m_left + paddingLeft, windowRect.m_top + paddingTop);

            Bitmap b = new(width, height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(topLeft, new Point(0, 0), new Size(width, height));
            b.Save(pathSaveFile, ImageFormat.Jpeg);
        }

        public static void ToClipbroad(IntPtr handle_, int paddingLeft = 8, int paddingTop = 2, int paddingRight = 8, int paddingBot = 8)
        {
            RECT windowRect = new();
            GetWindowRect(handle_, ref windowRect);

            int width = windowRect.m_right - windowRect.m_left - paddingLeft - paddingRight;
            int height = windowRect.m_bottom - windowRect.m_top - paddingTop - paddingBot;
            Point topLeft = new(windowRect.m_left + paddingLeft, windowRect.m_top + paddingTop);

            Bitmap b = new(width, height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(topLeft, new Point(0, 0), new Size(width, height));

            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(b.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            System.Windows.Clipboard.SetImage(bitmapSource);
        }
    }
}
