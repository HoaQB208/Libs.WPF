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
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(MenuRadioButton), new PropertyMetadata(default(PackIconKind)));
        public PackIconKind Kind
        {
            get => (PackIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        // MainContent
        public static readonly DependencyProperty MainContentProperty = DependencyProperty.Register(nameof(MainContent), typeof(string), typeof(MenuRadioButton), new PropertyMetadata(string.Empty));
        public string MainContent
        {
            get => (string)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        // SubContent
        public static readonly DependencyProperty SubContentProperty = DependencyProperty.Register(nameof(SubContent), typeof(string), typeof(MenuRadioButton), new PropertyMetadata(string.Empty));
        public string SubContent
        {
            get => (string)GetValue(SubContentProperty);
            set => SetValue(SubContentProperty, value);
        }

        // SubContentVisibility
        public static readonly DependencyProperty SubContentVisibilityProperty = DependencyProperty.Register(nameof(SubContentVisibility), typeof(Visibility), typeof(MenuRadioButton), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public Visibility SubContentVisibility
        {
            get => (Visibility)GetValue(SubContentVisibilityProperty);
            set => SetValue(SubContentVisibilityProperty, value);
        }

        // IsChecked
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked), typeof(bool?), typeof(MenuRadioButton), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public bool? IsChecked
        {
            get => (bool?)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        // GroupName
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register(nameof(GroupName), typeof(string), typeof(MenuRadioButton), new PropertyMetadata("MenuGroup"));
        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        // Size
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof(Size), typeof(double), typeof(MenuRadioButton), new PropertyMetadata(36.0));
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}