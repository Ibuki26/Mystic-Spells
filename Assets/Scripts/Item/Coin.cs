using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int score;

    protected override void Execute(WizardPresenter wizard)
    {
        AudioManager.Instance.PlaySE(AudioType.Coin);
        UIManager.Instance.AddScore(score);
    }
}
