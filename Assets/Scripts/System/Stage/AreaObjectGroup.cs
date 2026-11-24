using Unity.VisualScripting;
using UnityEngine;

//エリア内の敵キャラクター、アイテムをまとめて管理するクラス
public class AreaObjectGroup : MonoBehaviour
{
    [SerializeField] private EnemyPresenter[] enemies;
    [SerializeField] private Item[] items;
    //[SerializeField] private Gimmick[] gimmicks;

    //エリア内のEnemyPresenterのManualStart関数を呼ぶ
    public void EnemyManualStart()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.ManualStart();
        }
    }

    //エリア内のEnemyPresenterのManualUpdate関数を呼ぶ
    public void EnemyManualUpdate()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.ManualUpdate();
        }
    }

    //エリア内のEnemyPresenterのManualFixedUpdate関数を呼ぶ
    public void EnemyManualFixedUpdate()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.ManualFixedUpdate();
        }
    }

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
