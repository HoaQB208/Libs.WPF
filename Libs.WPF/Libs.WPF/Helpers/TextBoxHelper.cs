using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using Button = System.Windows.Controls.Button;
using RichTextBox = System.Windows.Controls.RichTextBox;
using TextBox = System.Windows.Controls.TextBox;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Libs.WPF.Helpers
{
    public class TextBoxHelper
    {
        #region Attached Properties
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(TextBoxHelper), new UIPropertyMetadata(false, OnIsMonitoringChanged));


        public static string GetWatermarkText(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkTextProperty);
        }

        public static void SetWatermarkText(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkTextProperty, value);
        }

        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.RegisterAttached("WatermarkText", typeof(string), typeof(TextBoxHelper), new UIPropertyMetadata(string.Empty));


        public static int GetTextLength(DependencyObject obj)
        {
            return (int)obj.GetValue(TextLengthProperty);
        }

        public static void SetTextLength(DependencyObject obj, int value)
        {
            obj.SetValue(TextLengthProperty, value);

            if (value >= 1)
            {
                obj.SetValue(HasTextProperty, true);
            }
            else
            {
                obj.SetValue(HasTextProperty, false);
            }

        }

        public static readonly DependencyProperty TextLengthProperty =
            DependencyProperty.RegisterAttached("TextLength", typeof(int), typeof(TextBoxHelper), new UIPropertyMetadata(0));

        #endregion

        private static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(TextBoxHelper), new FrameworkPropertyMetadata(false));

        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }

        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, value);
        }

        public static readonly DependencyProperty ButtonCommandProperty = DependencyProperty.RegisterAttached("ButtonCommand", typeof(ICommand), typeof(TextBoxHelper), new FrameworkPropertyMetadata(null));

        public static ICommand GetButtonCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ButtonCommandProperty);
        }

        public static void SetButtonCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ButtonCommandProperty, value);
        }

        public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsClearTextButtonBehaviorEnabled", typeof(bool), typeof(TextBoxHelper), new FrameworkPropertyMetadata(IsClearTextButtonBehaviorEnabledChanged));
        public static bool GetIsClearTextButtonBehaviorEnabled(Button d)
        {
            return (bool)d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
        }
        public static void SetIsClearTextButtonBehaviorEnabled(Button obj, bool value)
        {
            obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, value);
        }
        public static readonly DependencyProperty ButtonCommandParameterProperty = DependencyProperty.RegisterAttached("ButtonCommandParameter", typeof(object), typeof(TextBoxHelper), new FrameworkPropertyMetadata(null));
        public static object GetButtonCommandParameter(DependencyObject d)
        {
            return (object)d.GetValue(ButtonCommandParameterProperty);
        }
        public static void SetButtonCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonCommandParameterProperty, value);
        }

        private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && d is Button button)
            {
                button.Click -= ButtonClicked;

                if ((bool)e.NewValue)
                {
                    button.Click += ButtonClicked;
                }
            }
        }

        private static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            DependencyObject parent = button.GetAncestors().FirstOrDefault(a => a is RichTextBox || a is TextBox || a is PasswordBox || a is ComboBox);

            ICommand command = GetButtonCommand(parent);
            object commandParameter = GetButtonCommandParameter(parent) ?? parent;
            if (command != null && command.CanExecute(commandParameter))
            {
                if (parent is TextBox textBox1)
                {
                    textBox1.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                }

                command.Execute(commandParameter);
            }
            if (parent is TextBox textBox)
            {
                textBox.Clear();
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                textBox.Focus();
            }
        }

        static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox)
            {
                TextBox txtBox = d as TextBox;
                if ((bool)e.NewValue)
                {

                    txtBox.TextChanged += TextChanged;
                }

                else
                {
                    txtBox.TextChanged -= TextChanged;
                }
            }
        }

        static void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            if (txtBox == null) return;

            SetTextLength(txtBox, txtBox.Text.Length);
        }

        public static bool GetIsClearText(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClearTextProperty);
        }

        public static void SetIsClearText(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClearTextProperty, value);
        }

        public static readonly DependencyProperty IsClearTextProperty =
         DependencyProperty.RegisterAttached("IsClearText", typeof(bool), typeof(TextBoxHelper), new UIPropertyMetadata(false));

    }
}