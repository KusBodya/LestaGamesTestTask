namespace backend.Domain;

public sealed class Weapon
{
    public string Name { get; }
    public int Damage { get; }
    public WeaponType Type { get; }

    public Weapon(string name, int damage, WeaponType type)
    {
        Name = name;
        Damage = damage;
        Type = type;
    }

    public override string ToString() => $"{Name} (урон {Damage}, {Type})";
}