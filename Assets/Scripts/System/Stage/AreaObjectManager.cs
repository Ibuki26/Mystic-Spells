using UnityEngine;

//シーン内のAreaObjectGroupクラスを管理するクラス
public class AreaObjectManager : MonoBehaviour
{
    [SerializeField] AreaObjectGroup[] areaObjectGroups;
    [SerializeField] GoalFlag goalFlag;

    public void ResetGroup()
    {
        if (areaObjectGroups != null)
        {
            foreach (var group in areaObjectGroups)
                group.ResetObjects();
        }

        if(goalFlag != null)
            goalFlag.ResetGimmick();
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
