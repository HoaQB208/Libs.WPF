using Libs.WPF.MVVM;

namespace Test.WPF.Class
{
    class SampleItem : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }

        public List<string> Skills { get; set; }

        private string _SelectedSkill;
        public string SelectedSkill
        {
            get { return _SelectedSkill; }
            set
            {
                _SelectedSkill = value;
                OnPropertyChanged(nameof(SelectedSkill));
            }
        }


        public SampleItem(int id, string name, bool isMale)
        {
            Id = id;
            Name = name;
            IsMale = isMale;
            Skills = new List<string>()
            {
                "Code",
                "AI",
                "Unreal"
            };

            _SelectedSkill = Skills[0];
        }
    }
}