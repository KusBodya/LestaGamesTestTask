
using backend.Domain;
using backend.Patterns;

namespace backend.Data
{
    public static class MonstersCatalog
    {
        public static List<Monster> All()
        {
            var list = new List<Monster>();

            var goblin = new Monster("Гоблин", new Stats(1, 1, 1), hp: 5,
                                     weapon: new Weapon("Нож", 2, WeaponType.Piercing),
                                     drop: WeaponsCatalog.Dagger);
            list.Add(goblin);

            var skeleton = new Monster("Скелет", new Stats(2, 2, 1), hp: 10,
                                       weapon: new Weapon("Кость", 2, WeaponType.Bludgeoning),
                                       drop: WeaponsCatalog.Club);
            skeleton.DefenseEffects.Add(new VulnerableIfWeaponTypeEffect(WeaponType.Bludgeoning, factor: 2));
            list.Add(skeleton);

            var slime = new Monster("Слайм", new Stats(3, 1, 2), hp: 8,
                                    weapon: new Weapon("Удар щупальцем", 1, WeaponType.Bludgeoning),
                                    drop: WeaponsCatalog.Spear);
            slime.DefenseEffects.Add(new SlashImmunityWeaponComponentEffect());
            list.Add(slime);

            var ghost = new Monster("Призрак", new Stats(1, 3, 1), hp: 6,
                                    weapon: new Weapon("Эктоплазма", 3, WeaponType.Piercing),
                                    drop: WeaponsCatalog.Sword);
            ghost.AttackEffects.Add(new SneakAttackEffect());
            list.Add(ghost);

            var golem = new Monster("Голем", new Stats(3, 1, 3), hp: 10,
                                    weapon: new Weapon("Каменный кулак", 1, WeaponType.Bludgeoning),
                                    drop: WeaponsCatalog.Axe);
            golem.DefenseEffects.Add(new StoneSkinEffect());
            list.Add(golem);

            var dragon = new Monster("Дракон", new Stats(3, 3, 3), hp: 20,
                                     weapon: new Weapon("Клык", 4, WeaponType.Slashing),
                                     drop: WeaponsCatalog.Legendary);
            dragon.AttackEffects.Add(new FireEveryNthTurnEffect(n: 3, extra: 3));
            list.Add(dragon);

            return list;
        }
    }
}
