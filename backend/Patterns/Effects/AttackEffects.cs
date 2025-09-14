namespace backend.Patterns
{
    public sealed class SneakAttackEffect : IAttackEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.Attacker.Stats.Agility > ctx.Defender.Stats.Agility) ctx.FlatBonus += 1;
        }
    }

    public sealed class ActionSurgeEffect : IAttackEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.TurnNumber == 1) ctx.FlatBonus += ctx.WeaponComponent;
        }
    }

    public sealed class RageEffect : IAttackEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.TurnNumber <= 3) ctx.FlatBonus += 2;
            else ctx.FlatBonus -= 1;
        }
    }

    public sealed class PoisonStacksEffect : IAttackEffect
    {
        public void Apply(AttackContext ctx)
        {
            if (ctx.TurnNumber > 1) ctx.FlatBonus += (ctx.TurnNumber - 1);
        }
    }

    public sealed class FireEveryNthTurnEffect : IAttackEffect
    {
        private readonly int _n;
        private readonly int _extra;
        public FireEveryNthTurnEffect(int n, int extra) { _n = n; _extra = extra; }
        public void Apply(AttackContext ctx)
        {
            if (ctx.TurnNumber % _n == 0) ctx.FlatBonus += _extra;
        }
    }
}