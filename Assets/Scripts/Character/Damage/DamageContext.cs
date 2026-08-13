using UnityEngine;

public readonly struct DamageContext
{
    public int Strength { get; }
    public int Power { get; }
    public DamageType Type { get; }

    public DamageContext(int strength, int power, DamageType type)
    {
        Strength = strength;
        Power = power;
        Type = type;
    }
}
