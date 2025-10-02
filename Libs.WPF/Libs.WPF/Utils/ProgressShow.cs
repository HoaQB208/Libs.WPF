using Libs.WPF.Controls.Windows;
using Libs.WPF.MVVM;
using System.Windows;

namespace Libs.WPF.Utils
{
    public class ProgressShow : ViewModelBase
    {
        Window _owner;
        public ProgressShow(Window owner = null)
        {
            _owner = owner;
        }

        // MainStep
        private string _MainStep;
        public string MainStep
        {
            get { return _MainStep; }
            set
            {
                _MainStep = value;
                OnPropertyChanged(nameof(MainStep));
            }
        }

        // SubStep
        private string _SubStep;
        public string SubStep
        {
            get { return _SubStep; }
            set
            {
                _SubStep = value;
                OnPropertyChanged(nameof(SubStep));
            }
        }


        private ProgressWindow _View;
        public ProgressWindow View
        {
            get
            {
                if (_View is null)
                {
                    _View = new ProgressWindow(_owner) { DataContext = this };
                }
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged(nameof(View));
            }
        }

        public void Show()
        {
            View?.Show();
        }

        public void Close()
        {
            View?.Close();
        }
    }
}
