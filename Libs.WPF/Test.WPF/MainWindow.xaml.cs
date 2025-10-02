using Libs.WPF.Controls.Windows;
using Libs.WPF.Utils;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var pro = new ProgressShow();
            pro.Show();

            do
            {

                pro.Status = DateTime.Now.ToString();
                await Task.Delay(1000);


            } while (true);
        }
    }
}