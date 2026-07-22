using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常時のダメージ計算
public class NormalDamage : IDamageStrategy
{
    private const int DefenseDivider = 4;

    public int CalculateDamage(int attackerStrength, int skillPower, int defense)
    {
        int damage = skillPower + (attackerStrength - defense) / DefenseDivider;
        
        return Mathf.Max(0, damage);
    }
}
