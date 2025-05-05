namespace Test.WPF.Class
{
    class SampleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }

        public List<string> Skills { get; set; }

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
        }
    }
}