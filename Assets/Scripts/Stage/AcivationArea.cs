using UnityEngine;

//当たり判定を使い敵キャラクターやギミックをアクティブ状態を管理するクラス
public class AcivationArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IActivationAreaReceiver>(out var receiver))
            receiver.EnterActivationArea();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IActivationAreaReceiver>(out var receiver))
            receiver.ExitActivationArea();
    }
}
