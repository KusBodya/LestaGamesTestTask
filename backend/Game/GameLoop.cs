using System;
using backend.Data;
using backend.Domain;

namespace backend.Game
{
    public static class GameLoop
    {
        private static readonly Random Rng = new();

        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                var (player, playerClass) = CreateNewPlayer();
                int streak = 0;

                while (true)
                {
                    var monsters = MonstersCatalog.All();
                    var monster = monsters[Rng.Next(monsters.Count)];
                    Console.WriteLine($"\n=== Бой со случайным противником: {monster.Name} ===");
                    Console.WriteLine(
                        $"Вы: {player.Name} [{player.Stats}] | HP {player.Health}/{player.MaxHealth} | {player.Weapon}");
                    Console.WriteLine(
                        $"Противник: {monster.Name} [{monster.Stats}] | HP {monster.Health}/{monster.MaxHealth} | {monster.Weapon}");

                    player.ResetBattle();
                    player.HealToFull();
                    monster.ResetBattle();
                    monster.HealToFull();

                    bool playerTurn = player.Stats.Agility >= monster.Stats.Agility;

                    while (player.IsAlive && monster.IsAlive)
                    {
                        if (playerTurn) ResolveTurn(player, monster);
                        else ResolveTurn(monster, player);
                        playerTurn = !playerTurn;
                    }

                    bool playerWon = player.IsAlive;
                    Console.WriteLine(playerWon ? "\nПобеда!" : "\nПоражение...");

                    if (playerWon)
                    {
                        streak++;
                        player.HealToFull();

                        Console.WriteLine($"Трофей: {monster.Drop}. Заменить текущее оружие? (y/n)");
                        var ans = Console.ReadLine()?.Trim().ToLowerInvariant();
                        if (ans == "y") player.Weapon = monster.Drop;

                        if (streak >= 5)
                        {
                            Console.WriteLine("\nВы победили 5 монстров подряд — игра пройдена!");
                            break;
                        }

                        if (player.Levels[playerClass] < 3)
                        {
                            player.GainLevel(playerClass);
                            Console.WriteLine(
                                $"Уровень повышен в классе {playerClass}. Текущий уровень: {player.Levels[playerClass]}. HP {player.MaxHealth}. Статы {player.Stats}");
                        }
                        else
                        {
                            Console.WriteLine("Максимальный уровень достигнут, здоровье восстановлено.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Создать нового персонажа? (y/n)");
                        var ans = Console.ReadLine()?.Trim().ToLowerInvariant();
                        if (ans == "y") break;
                        else return;
                    }
                }
            }
        }

        private static void ResolveTurn(Actor a, Actor b)
        {
            if (a.TryAttack(b, Rng, out int dealt, out bool miss))
                Console.WriteLine($"{a.Name} атакует {b.Name} и наносит {dealt} урона (HP {b.Health}/{b.MaxHealth})");
            else
                Console.WriteLine(miss
                    ? $"{a.Name} атакует {b.Name}, но промахивается"
                    : $"{a.Name} атакует {b.Name}, урона нет");
        }

        private static (PlayerCharacter player, ClassId playerClass) CreateNewPlayer()
        {
            int str = Rng.Next(1, 4), agi = Rng.Next(1, 4), end = Rng.Next(1, 4);
            Console.WriteLine("Создание персонажа. Выберите класс: 1) Воин  2) Варвар  3) Разбойник");
            var pick = Console.ReadLine();

            ClassId cls = pick switch
            {
                "1" => ClassId.Warrior,
                "2" => ClassId.Barbarian,
                "3" => ClassId.Rogue,
                _ => ClassId.Warrior
            };

            var stats = new Stats(str, agi, end);
            var player = new PlayerCharacter("Игрок", stats, PlayerCharacter.StartingWeapon(cls));
            player.GainLevel(cls);

            Console.WriteLine(
                $"Стартовые статы: {stats}. Класс: {cls}. HP {player.MaxHealth}. Оружие: {player.Weapon}");
            return (player, cls);
        }
    }
}
