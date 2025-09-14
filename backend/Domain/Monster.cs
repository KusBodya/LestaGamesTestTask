

namespace backend.Domain
{
    public sealed class Monster : Actor
    {
        public Weapon Drop { get; }
        public Monster(string name, Stats stats, int hp, Weapon weapon, Weapon drop)
            : base(name, stats, hp, weapon) { Drop = drop; }
    }
}