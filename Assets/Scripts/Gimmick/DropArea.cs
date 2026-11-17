using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    [SerializeField] private int damage;

    private const int DummyDirection = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<WizardPresenter>(out var wizard))
        {
            //プレイヤーを指定の位置に戻し、ダメージを与える
            //カメラの位置を移動する
            wizard.transform.position = pos;

            var context = new DamageContext(0, damage, DummyDirection, DamageType.Fixed, null);
            wizard.TakeDamage(context);
        }

        if(collision.gameObject.TryGetComponent<WizardAgent>(out var agent))
        {
            var reward = RewardManager.Instance.FallIntoHoleReward;
            agent.AddReward(reward);
            UIManager.Instance.AddLog($"fall into hole: {reward}", Color.blue);
        }

        if (collision.gameObject.TryGetComponent<EnemyPresenter>(out var enemy))
        {
            //スコアの加点とEnemyの破棄
            Destroy(enemy);
        }
    }
}
