using Libs.WPF.Controls.PackIcons;
using System.Windows;

namespace Libs.WPF.Controls.Menu
{
    public partial class MenuRadioButton : System.Windows.Controls.UserControl
    {
        public MenuRadioButton()
        {
            InitializeComponent();
        }

        // Kind
        public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(MenuRadioButton), new PropertyMetadata(default(PackIconKind)));

        public PackIconKind Kind
        {
            get => (PackIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        // ContentEN
        public static readonly DependencyProperty ContentENProperty =
            DependencyProperty.Register(nameof(ContentEN), typeof(string), typeof(MenuRadioButton), new PropertyMetadata(string.Empty));

        public string ContentEN
        {
            get => (string)GetValue(ContentENProperty);
            set => SetValue(ContentENProperty, value);
        }

        // ContentVI
        public static readonly DependencyProperty ContentVIProperty =
            DependencyProperty.Register(nameof(ContentVI), typeof(string), typeof(MenuRadioButton), new PropertyMetadata(string.Empty));

        public string ContentVI
        {
            get => (string)GetValue(ContentVIProperty);
            set => SetValue(ContentVIProperty, value);
        }

        // IsChecked
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool?), typeof(MenuRadioButton), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool? IsChecked
        {
            get => (bool?)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        // GroupName
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register(nameof(GroupName), typeof(string), typeof(MenuRadioButton), new PropertyMetadata("MenuGroup"));

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        // Size
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(double), typeof(MenuRadioButton), new PropertyMetadata(42.0));

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}