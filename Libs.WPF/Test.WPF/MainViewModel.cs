using Libs.WPF.MVVM;
using Test.WPF.Class;

namespace Test.WPF
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _SelectedStringItem = StringList[0];
        }

        public List<string> StringList { get; } = new List<string>()
        {
            "Sample 1",
            "Sample 2",
            "Sample 3",
            "Sample 4",
            "Sample 5",
            "Sample 6",
            "Sample 7",
        };

        private string _SelectedStringItem;
        public string SelectedStringItem
        {
            get { return _SelectedStringItem; }
            set
            {
                _SelectedStringItem = value;
                OnPropertyChanged(nameof(SelectedStringItem));
            }
        }

        public List<SampleItem> SampleItems { get; } = new List<SampleItem>()
        {
            new(1, "Pham Thai Hoa", true),
            new(2, "Nguyễn Thị B", false),
            new(10, "Trần Văn Nam", true),
        };
    }
}