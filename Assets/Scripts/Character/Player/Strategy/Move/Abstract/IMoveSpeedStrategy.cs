using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//速度計算の方法を定義するインターフェース
public interface IMoveSpeedStrategy
{
    //速度の計算を行う
    public float CalculateMoveSpeed(float speed);
}
