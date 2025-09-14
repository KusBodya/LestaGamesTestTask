using System;
using System.Collections.Generic;
using backend.Patterns;
using backend.Domain;
using backend.Domain;

namespace backend.Domain
{
    public abstract class Actor
    {
        public string Name { get; protected set; }
        public Stats Stats { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public Weapon Weapon { get; set; }
        public List<IAttackEffect> AttackEffects { get; } = new();
        public List<IDefenseEffect> DefenseEffects { get; } = new();
        public int TurnsThisBattle { get; set; } = 0;

        protected Actor(string name, Stats stats, int maxHealth, Weapon weapon)
        { Name = name; Stats = stats; MaxHealth = maxHealth; Health = maxHealth; Weapon = weapon; }

        public bool IsAlive => Health > 0;
        public void HealToFull() => Health = MaxHealth;
        public void ResetBattle() => TurnsThisBattle = 0;

        public bool TryAttack(Actor target, Random rng, out int dealt, out bool miss)
        {
            dealt = 0; miss = false;

            int roll = rng.Next(1, Stats.Agility + target.Stats.Agility + 1);
            if (roll <= target.Stats.Agility) { miss = true; TurnsThisBattle++; return false; }

            var ctx = new AttackContext(this, target, Weapon.Type,
                weaponComponent: Weapon.Damage,
                flatBonus: Stats.Strength,
                turnNumber: TurnsThisBattle + 1);

            foreach (var eff in AttackEffects) eff.Apply(ctx);
            foreach (var eff in target.DefenseEffects) eff.Apply(ctx);

            dealt = Math.Max(0, ((ctx.WeaponComponent + ctx.FlatBonus) * ctx.Multiplier) - ctx.FlatReduction);
            if (dealt > 0) target.Health = Math.Max(0, target.Health - dealt);

            TurnsThisBattle++;
            return dealt > 0;
        }
    }
}