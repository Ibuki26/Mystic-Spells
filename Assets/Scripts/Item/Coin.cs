using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<WizardPresenter>(out var _))
        {
            AudioManager.Instance.PlaySE(AudioType.Coin);
            UIManager.Instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}
