using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ActivationArea への進入・退出通知を受け取るオブジェクトが実装するインターフェース。
public interface IActivationAreaReceiver
{
    // ActivationArea に進入したときに呼び出される。
    public void EnterActivationArea();

    // ActivationArea から退出したときに呼び出される。
    public void ExitActivationArea();
}
