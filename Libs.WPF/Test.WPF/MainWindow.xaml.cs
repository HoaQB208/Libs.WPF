using Libs.WPF.Controls.Windows;
using System.Windows;

namespace Test.WPF
{
    public partial class MainWindow : DarkWindow
    {
        public MainWindow()
        {
            IsShowSnapshotButton = true;
            InitializeComponent();

            this.DataContext = new MainViewModel();

            this.Height = SystemParameters.PrimaryScreenHeight * 0.7;
            this.Width = this.Height * 1.618;
        }
    }
}