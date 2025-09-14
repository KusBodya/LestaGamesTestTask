using backend.Domain;

namespace backend.Patterns
{
    public sealed class ShieldEffect : IDefenseEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.Defender.Stats.Strength > ctx.Attacker.Stats.Strength) ctx.FlatReduction += 3;
        }
    }

    public sealed class StoneSkinEffect : IDefenseEffect
    {
        public void Apply(AttackContext ctx)
        {
            ctx.FlatReduction += ctx.Defender.Stats.Endurance;
        }
    }

    public sealed class SlashImmunityWeaponComponentEffect : IDefenseEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.WeaponType == WeaponType.Slashing) ctx.WeaponComponent = 0;
        }
    }

    public sealed class VulnerableIfWeaponTypeEffect : IDefenseEffect
    {
        private readonly WeaponType _type; private readonly int _factor;
        public VulnerableIfWeaponTypeEffect(WeaponType type, int factor) { _type = type; _factor = factor; }
        public void Apply(AttackContext ctx)
        {
            if (ctx.WeaponType == _type) ctx.Multiplier *= _factor;
        }
    }
}