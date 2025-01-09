namespace ExampleExam
{
    public class PlayGround
    {
        private int _id;
        private string _name;
        private int _maxChildren;
        private int _minAge;

        public PlayGround(int id, string name, int maxChildren, int minAge)
        {
            Id = id;
            Name = name;
            MaxChildren = maxChildren;
            MinAge = minAge;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int MaxChildren { get => _maxChildren; set => _maxChildren = value; }
        public int MinAge { get => _minAge; set => _minAge = value; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(MaxChildren)}={MaxChildren.ToString()}, {nameof(MinAge)}={MinAge.ToString()}}}";
        }
    }
}
