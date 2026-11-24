using UnityEngine;
using Unity.MLAgents;

//報酬の値を管理するクラス。ここから報酬を与えるときに値を渡す
public class RewardManager : SingletonMonoBehaviour<RewardManager>
{
    [SerializeField] private AreaObjectManager areaManager;
    //報酬が定数
    [field: SerializeField] public float EnemyContactReward { get; private set; } = -0.01f;
    [field: SerializeField] public float GoalReward { get; private set; } = 1.0f;
    [field: SerializeField] public float GameOverReward { get; private set; } = -1.0f;
    [field: SerializeField] public float FallIntoHoleReward { get; private set; } = -0.01f;
    //報酬がシーンの内容によって変わる
    public float EnemyDefeatReward { get; private set; } = 0.01f;
    public float ItemCollectReward { get; private set; } = 0.01f;
    public float NewGimmcikContactReward { get; private set; } = 0.01f;

    //private EnvironmentParameters _envParams;
    private int _enemyCount = 0;
    private int _itemCount = 0;
    private int _gimmickCount = 0;

    protected override void Awake()
    {
        base.Awake();
        //_envParams = Academy.Instance.EnvironmentParameters;
        //SetEnemyDefeatReward();
        //SetItemCollectReward();
        //SetNewGimmcikContactReward();
        _enemyCount = areaManager.GetEnemyCount();
        Debug.Log($"enemy: {_enemyCount}");
        _itemCount = areaManager.GetItemCount();
        Debug.Log($"item: {_itemCount}");
    }

    //敵の数を集計し、EnemyDefeatRewardの値を計算する
    private void SetEnemyDefeatReward()
    {
        _enemyCount = areaManager.GetEnemyCount();
        Debug.Log($"enemy: {_enemyCount}");
        EnemyDefeatReward = 1f / _enemyCount;
        Debug.Log($"EnemyDefeatReward: {EnemyDefeatReward}");
    }

    //アイテムの数を集計し、ItemCollectRewardの値を計算する
    private void SetItemCollectReward()
    {
        _itemCount = areaManager.GetItemCount();
        Debug.Log($"item: {_itemCount}");
        ItemCollectReward = 1f / _itemCount;
        Debug.Log($"ItemCollectReward: {ItemCollectReward}");
    }
    /*
    //ギミックの数を集計し、NewGimmcikContactRewardの値を計算する
    private void SetNewGimmcikContactReward()
    {
        _gimmickCount = areaManager.GetGimmickCount();
        Debug.Log($"gimmick: {_gimmickCount}");
        NewGimmcikContactReward = 1f / _gimmickCount;
        Debug.Log($"NewGimmcikContactReward: {NewGimmcikContactReward}");
    }
    */
}
