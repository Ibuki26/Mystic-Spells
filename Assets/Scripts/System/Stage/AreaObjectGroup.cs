using Unity.VisualScripting;
using UnityEngine;

//エリア内の敵キャラクター、アイテムをまとめて管理するクラス
public class AreaObjectGroup : MonoBehaviour
{
    [SerializeField] private EnemyPresenter[] enemies;
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject[] blocks;
    //[SerializeField] private Gimmick[] gimmicks;

    //エリア内のEnemyPresenterのManualStart関数を呼ぶ
    public void EnemiesManualStart()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.ManualStart();
            }
        }
    }

    //エリア内のEnemyPresenterのManualUpdate関数を呼ぶ
    public void EnemiesManualUpdate()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.ManualUpdate();
            }
        }
    }

    //エリア内のEnemyPresenterのManualFixedUpdate関数を呼ぶ
    public void EnemiesManualFixedUpdate()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.ManualFixedUpdate();
            }
        }
    }

    //ステージ内のオブジェクトの初期化
    public void ResetObjects()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if(enemy != null)
                    enemy.ResetEnemy();
            }
                
        }

        if (items != null && items.Length > 0)
        {
            foreach (var item in items)
            {
                if (item != null)
                    item.ResetItem();
            }
        }
        /*
        if(gimmicks != null && gimmicks.Length > 0)
        {
            foreach (var gimmick in gimmicks)
                gimmick.ResetGimmick();
        }
        */
    }

    //エリア内の敵キャラクターのアクティブ状態を設定する
    public void SetEnemiesActive(bool value)
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.gameObject.SetActive(value);
            }
        }
    }

    //エリア内のアイテムのアクティブ状態を設定する
    public void SetItemsActive(bool value)
    {
        if (items != null && items.Length > 0)
        {
            foreach (var item in items)
            {
                if (item != null)
                    item.gameObject.SetActive(value);
            }
        }
    }

    //エリア内の埋め立てブロックのアクティブ状態を設定する
    public void SetBlocksActive(bool value)
    {
        if(blocks != null && blocks.Length > 0)
        {
            foreach(var block in blocks)
            {
                if (block != null)
                    block.SetActive(value);
            }
        }
    }

    //エリア内の敵キャラクターの数を返す
    public int GetEnemyCount() => enemies.Length;

    //エリア内のアイテムの数を返す
    public int GetItemCount() => items.Length;

    //エリア内のギミックの数を返す
    //public int GetGimmickCount() => gimmicks.Length;
}
