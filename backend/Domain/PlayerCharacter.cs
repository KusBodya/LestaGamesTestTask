using System.Collections.Generic;
using System.Linq;
using backend.Data;
using backend.Domain;
using backend.Patterns;


namespace backend.Domain
{
    public sealed class PlayerCharacter : Actor
    {
        public Dictionary<ClassId, int> Levels { get; } = new()
        { { ClassId.Warrior, 0 }, { ClassId.Barbarian, 0 }, { ClassId.Rogue, 0 } };

        public int TotalLevel => Levels.Values.Sum();

        public PlayerCharacter(string name, Stats stats, Weapon startWeapon)
            : base(name, stats, 0, startWeapon) { }

        public void GainLevel(ClassId cls)
        {
            Levels[cls]++;

            int hpGain = ClassHealthPerLevel(cls) + Stats.Endurance;
            MaxHealth += hpGain;
            Health = MaxHealth;

            switch (cls)
            {
                case ClassId.Rogue:
                    if (Levels[cls] == 1) AttackEffects.Add(new SneakAttackEffect());
                    if (Levels[cls] == 2) Stats.Add(0, 1, 0);
                    if (Levels[cls] == 3) AttackEffects.Add(new PoisonStacksEffect());
                    break;

                case ClassId.Warrior:
                    if (Levels[cls] == 1) AttackEffects.Add(new ActionSurgeEffect());
                    if (Levels[cls] == 2) DefenseEffects.Add(new ShieldEffect());
                    if (Levels[cls] == 3) Stats.Add(1, 0, 0);
                    break;

                case ClassId.Barbarian:
                    if (Levels[cls] == 1) AttackEffects.Add(new RageEffect());
                    if (Levels[cls] == 2) DefenseEffects.Add(new StoneSkinEffect());
                    if (Levels[cls] == 3) Stats.Add(0, 0, 1);
                    break;
            }
        }

        public static int ClassHealthPerLevel(ClassId cls) => cls switch
        {
            ClassId.Rogue => 4,
            ClassId.Warrior => 5,
            ClassId.Barbarian => 6,
            _ => 4
        };

        public static Weapon StartingWeapon(ClassId cls) => cls switch
        {
            ClassId.Rogue => WeaponsCatalog.Dagger,
            ClassId.Warrior => WeaponsCatalog.Sword,
            ClassId.Barbarian => WeaponsCatalog.Club,
            _ => WeaponsCatalog.Dagger
        };
    }
}
