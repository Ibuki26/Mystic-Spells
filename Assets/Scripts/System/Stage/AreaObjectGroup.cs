using UnityEngine;

//エリア内の敵キャラクター、アイテムをまとめて管理するクラス
public class AreaObjectGroup : MonoBehaviour
{
    [SerializeField] private EnemyPresenter[] enemies;
    [SerializeField] private Item[] items;
    //[SerializeField] private Gimmick[] gimmicks;

    //ステージ内のオブジェクトの初期化
    public void ResetObjects()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
                enemy.ResetEnemy();
        }

        if (items != null && items.Length > 0)
        {
            foreach (var item in items)
                item.ResetItem();
        }
        /*
        if(gimmicks != null && gimmicks.Length > 0)
        {
            foreach (var gimmick in gimmicks)
                gimmick.ResetGimmick();
        }
        */
    }

    //エリア内の敵キャラクターの数を返す
    public int GetEnemyCount() => enemies.Length;

    //エリア内のアイテムの数を返す
    public int GetItemCount() => items.Length;

    //エリア内のギミックの数を返す
    //public int GetGimmickCount() => gimmicks.Length;
}
