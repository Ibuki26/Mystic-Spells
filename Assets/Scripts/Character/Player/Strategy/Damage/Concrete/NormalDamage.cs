using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常時のダメージ計算
public class NormalDamage : IDamageStrategy
{
    private const int DefenseDivider = 4;

    public int CalculateDamage(DamageContext context, int defense)
    {
        switch (context.Type)
        {
            case DamageType.Normal:
                return Mathf.Max(0, context.Power + (context.Strength - defense) / DefenseDivider);
                
            case DamageType.Fixed:
                return context.Power;

            default:
                Debug.LogError($"未対応のDamageType : {context.Type}");
                return 0;
        }
    }
}
