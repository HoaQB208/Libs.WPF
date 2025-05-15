using System.Windows.Media;
using System.Windows;

namespace Libs.WPF.Controls.Media
{
    static class VisualTreeModule
    {
        public static FrameworkElement FindChild(DependencyObject obj, string childName)
        {
            if (obj == null) return null!;

            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            queue.Enqueue(obj);

            while (queue.Count > 0)
            {
                obj = queue.Dequeue();

                int childCount = VisualTreeHelper.GetChildrenCount(obj);
                for (int i = 0; i < childCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child is FrameworkElement fe && fe.Name == childName)
                    {
                        return fe;
                    }
                    queue.Enqueue(child);
                }
            }

            return null!;
        }
    }
}