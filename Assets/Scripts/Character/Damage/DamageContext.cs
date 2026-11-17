using UnityEngine;

public readonly struct DamageContext
{
    public int Strength { get; }
    public int Power { get; }
    public int Direction { get; }
    public DamageType Type { get; }

    public WizardAgent Agent { get; }

    public DamageContext(int strength, int power, int direction, DamageType type, WizardAgent agent)
    {
        Strength = strength;
        Power = power;
        Direction = direction;
        Type = type;
        Agent = agent;
    }
}
