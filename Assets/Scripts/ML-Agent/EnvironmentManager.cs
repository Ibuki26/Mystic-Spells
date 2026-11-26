using UnityEngine;

//カリキュラム学習でステージ内のオブジェクトを制御するクラス
public class EnvironmentManager
{
    public void SetEnvironmentByLesson(int lesson)
    {
        var areaObjectManager = AreaObjectManager.Instance;

        switch (lesson)
        {
            //ゴールのみ
            case 0:
                areaObjectManager.SetEnemiesActive(false);
                areaObjectManager.SetItemsActive(false);
                areaObjectManager.SetBlocksActive(true);
                break;
            //穴、ゴール
            case 1:
                areaObjectManager.SetEnemiesActive(false);
                areaObjectManager.SetItemsActive(false);
                areaObjectManager.SetBlocksActive(false);
                break;
            //穴、アイテム、ゴール
            case 2:
                areaObjectManager.SetEnemiesActive(false);
                areaObjectManager.SetItemsActive(true);
                areaObjectManager.SetBlocksActive(false);
                break;
            //穴、敵、ゴール
            case 3:
                areaObjectManager.SetEnemiesActive(true);
                areaObjectManager.SetItemsActive(false);
                areaObjectManager.SetBlocksActive(false);
                break;
            //穴、敵、アイテム、ゴール
            case 4:
                areaObjectManager.SetEnemiesActive(true);
                areaObjectManager.SetItemsActive(true);
                areaObjectManager.SetBlocksActive(false);
                break;
            //対応数値以外が出た時の対応
            default:
                Debug.LogWarning($"未対応のレッスン番号です: {lesson}");
                break;
        }
    }
}
