﻿using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;
using System;

namespace Libs.WPF.Animations
{
    public class MinimizeAnimation
    {
        public static async Task OpacityAndMoveDown(Window window, int milliSecondsDuration = 500)
        {
            // Làm mờ dần
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(milliSecondsDuration)
            };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(opacityAnimation, window);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();

            // Tạo TranslateTransform để di chuyển cửa sổ xuống dưới
            TranslateTransform translateTransform = new TranslateTransform();
            window.RenderTransform = translateTransform;
            DoubleAnimation translateYAnimation = new DoubleAnimation()
            {
                From = 0,
                To = window.ActualHeight,
                Duration = TimeSpan.FromMilliseconds(milliSecondsDuration)
            };
            translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnimation);
            await Task.Delay(milliSecondsDuration);
        }

        public static void OpacityAndMoveDown_Restore(Window window)
        {
            // Khôi phục làm mờ
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(10)
            };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(opacityAnimation, window);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();

            // Khôi phục Y
            TranslateTransform translateTransform = new TranslateTransform();
            window.RenderTransform = translateTransform;
            DoubleAnimation translateYAnimation = new DoubleAnimation()
            {
                From = window.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(10)
            };
            translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnimation);
        }
    }
}