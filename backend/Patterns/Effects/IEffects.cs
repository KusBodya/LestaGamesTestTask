using backend.Domain;

namespace backend.Patterns
{
    public sealed class AttackContext
    {
        public Actor Attacker { get; }
        public Actor Defender { get; }
        public WeaponType WeaponType { get; }
        public int WeaponComponent { get; set; }
        public int FlatBonus { get; set; }
        public int FlatReduction { get; set; } = 0;
        public int Multiplier { get; set; } = 1;
        public int TurnNumber { get; }

        public AttackContext(Actor a, Actor d, WeaponType t, int weaponComponent, int flatBonus, int turnNumber)
        { Attacker = a; Defender = d; WeaponType = t; WeaponComponent = weaponComponent; FlatBonus = flatBonus; TurnNumber = turnNumber; }
    }

    public interface IAttackEffect
    {
        void Apply(AttackContext ctx);
    }

    public interface IDefenseEffect
    {
        void Apply(AttackContext ctx);
    }
}