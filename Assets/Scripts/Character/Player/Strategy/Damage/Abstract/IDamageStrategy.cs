using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージ計算の方法を定義するインターフェース
public interface IDamageStrategy
{
    //ダメージの計算を行う
    public int CalculateDamage(int attackerStrength, int skillPower, int defense);
}
