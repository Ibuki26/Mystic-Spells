using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回復量の計算を定義するインターフェース
public interface IHealStrategy
{
    //回復量の計算を行う
    public int CalculateHeal(int healPower);
}
