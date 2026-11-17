using UnityEngine;

public class NormalHealItem : Item
{
    [SerializeField] private int healPower;

    protected override void Execute(WizardPresenter wizard)
    {
        AudioManager.Instance.PlaySE(AudioType.Heal);
        wizard.Model.TakeHeal(healPower);
    }
}
