using UnityEngine;

public class NormalHealItem : MonoBehaviour
{
    [SerializeField] private int healPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<WizardPresenter>(out var wizard))
        {
            AudioManager.Instance.PlaySE(AudioType.Heal);
            wizard.Model.TakeHeal(healPower);
            Destroy(gameObject);
        }
    }
}
