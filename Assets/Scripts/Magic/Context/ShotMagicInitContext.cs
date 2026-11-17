using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ShotMagicƒNƒ‰ƒX‚ÌInitilizeŠÖ”—p‚ÌContext
public readonly struct ShotMagicInitContext
{
    public int Power { get; }
    public int Strength { get; }
    public float Speed { get; }
    public int Direction { get; }
    public float Duration { get; }
    public WizardAgent Agent { get; }

    public ShotMagicInitContext( int power, int strength, float speed,int direction, float duration, WizardAgent agent)
    {
        Speed = speed;
        Power = power;
        Strength = strength;
        Direction = direction;
        Duration = duration;
        Agent = agent;
    }
}
