using UnityEngine;
using System;

[Flags]
public enum WizardStateFlags
{
    Standing = 1 << 0, //地面の上に立っている状態
    Jumping = 1 << 1, //ジャンプしている状態
    Casting = 1 << 2, //魔法発動中
    Invincible = 1 << 3, //ダメージ無視状態
    TouchingCeiling = 1 << 4 //天井とぶつかっている状態
}
