using UnityEngine;

//ギミックの親クラス
public class Gimmick : MonoBehaviour
{
    private bool _isTouched = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isTouched) return;
        
        //プレイヤーが触れたら報酬を与え、触れたことを記録する
        if(collision.gameObject.TryGetComponent<WizardAgent>(out var agent))
        {
            _isTouched = true;
            var reward = RewardManager.Instance.NewGimmcikContactReward;
            agent.AddReward(reward);
            UIManager.Instance.AddLog($"contact new gimmick: {reward}");
        }
    }

    //初期化
    public void ResetGimmick()
    {
        _isTouched = false;
    }
}
