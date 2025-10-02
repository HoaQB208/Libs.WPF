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

        // Status
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
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
    }
}
