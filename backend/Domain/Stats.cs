namespace backend.Domain
{
    public sealed class Stats
    {
        public int Strength { get; private set; }
        public int Agility { get; private set; }
        public int Endurance { get; private set; }
        public Stats(int strength, int agility, int endurance) { Strength = strength; Agility = agility; Endurance = endurance; }
        public void Add(int str, int agi, int end) { Strength += str; Agility += agi; Endurance += end; }
        
        public Stats Clone() => new Stats(Strength, Agility, Endurance);
        
        public override string ToString() => $"Сила {Strength}, Ловк {Agility}, Выносл {Endurance}";
    }
}
