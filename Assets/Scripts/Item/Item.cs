using UnityEngine;

//アイテムの抽象クラス
public abstract class Item : MonoBehaviour
{
    private bool _isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isCollected) return;

        if (!collision.TryGetComponent<WizardAgent>(out var agent)) return;
        if (!collision.TryGetComponent<WizardPresenter>(out var wizard)) return;

        _isCollected = true;

        var reward = RewardManager.Instance.ItemCollectReward;
        agent.AddReward(reward);
        UIManager.Instance.AddLog($"collect item: {reward}");

        Execute(wizard);
        gameObject.SetActive(false);
    }

    //それぞれのアイテムの効果を継承先で書く
    protected abstract void Execute(WizardPresenter wizard);

    //初期化
    public void ResetItem()
    {
        _isCollected = false;
        gameObject.SetActive(true);
    }
}
