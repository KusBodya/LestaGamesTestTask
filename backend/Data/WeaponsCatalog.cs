using backend.Domain;

namespace backend.Data
{
    public static class WeaponsCatalog
    {
        public static readonly Weapon Sword     = new("Меч", 3, WeaponType.Slashing);
        public static readonly Weapon Club      = new("Дубина", 3, WeaponType.Bludgeoning);
        public static readonly Weapon Dagger    = new("Кинжал", 2, WeaponType.Piercing);
        public static readonly Weapon Axe       = new("Топор", 4, WeaponType.Slashing);
        public static readonly Weapon Spear     = new("Копьё", 3, WeaponType.Piercing);
        public static readonly Weapon Legendary = new("Легендарный Меч", 10, WeaponType.Slashing);
    }
}