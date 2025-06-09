using System.Windows.Media.Animation;
using System.Windows;
using System;

namespace Libs.WPF.Animations
{
    public class LoadedAnimation
    {
        public static void WidthAndOpacity(Window window, double secondsDuration = 0.4)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation()
            {
                From = 0,
                To = window.Width,
                Duration = TimeSpan.FromSeconds(secondsDuration)
            };
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
            Storyboard.SetTarget(widthAnimation, window);

            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(secondsDuration * 2)
            };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(opacityAnimation, window);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(widthAnimation);
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }

        public static void Opacity(Window window, double secondsDuration = 0.4)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(secondsDuration * 2)
            };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(opacityAnimation, window);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }
    }
}