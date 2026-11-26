using UnityEngine;

//シーン内のAreaObjectGroupクラスを管理するクラス
public class AreaObjectManager : SingletonMonoBehaviour<AreaObjectManager>
{
    [SerializeField] AreaObjectGroup[] areaObjectGroups;
    [SerializeField] GoalFlag goalFlag;

    //シーン内のEnemyPresenterのManualStart関数を呼ぶ
    public void EnemyManualStart()
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.EnemiesManualStart();
            }
        }
    }

    //シーン内のEnemyPresenterのManualUpdate関数を呼ぶ
    public void EnemyManualUpdate()
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.EnemiesManualUpdate();
            }
        }
    }

    //シーン内のEnemyPresenterのManualFixedUpdate関数を呼ぶ
    public void EnemyManualFixedUpdate()
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.EnemiesManualFixedUpdate();
            }
        }
    }

    //シーン内のオブジェクトの変数、座標、アクティブ状態をリセットする
    public void ResetGroup()
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if(group != null)
                    group.ResetObjects();
            }
        }

        if(goalFlag != null)
            goalFlag.ResetGimmick();
    }

    //シーン内の敵キャラクターのアクティブ状態を設定する
    public void SetEnemiesActive(bool value)
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.SetEnemiesActive(value);
            }
        }
    }

    //シーン内のアイテムのアクティブ状態を設定する
    public void SetItemsActive(bool value)
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.SetItemsActive(value);
            }
        }
    }

    //シーン内のブロックのアクティブ状態を設定する
    public void SetBlocksActive(bool value)
    {
        if (areaObjectGroups != null && areaObjectGroups.Length > 0)
        {
            foreach (var group in areaObjectGroups)
            {
                if (group != null)
                    group.SetBlocksActive(value);
            }
        }
    }

    //シーン内の敵キャラクターの数を返す
    public int GetEnemyCount()
    {
        int count = 0;

        foreach (var group in areaObjectGroups)
            count += group.GetEnemyCount();

        return count;
    }

    //シーン内のアイテムの数を返す
    public int GetItemCount()
    {
        int count = 0;

        foreach (var group in areaObjectGroups)
            count += group.GetItemCount();

        return count;
    }
    /*
    //シーン内のギミックの数を返す
    public int GetGimmickCount()
    {
        int count = 0;

        foreach (var group in areaObjectGroups)
            count += group.GetGimmickCount();

        return count;
    }
    */
}
