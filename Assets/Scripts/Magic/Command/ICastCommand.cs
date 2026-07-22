using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//魔法の発動処理とクールタイムの確認法を定義するインターフェース
//実装クラスは魔法の種類ごとの挙動を持つ
public interface ICastCommand
{
    public void Execute(IMagicConfig config, CastContext castContext);

    public bool IsReady();
}
