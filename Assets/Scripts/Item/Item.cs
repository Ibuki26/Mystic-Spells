using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<WizardPresenter>(out var wizard))
        {
            Excute(wizard);
            gameObject.SetActive(false);
        }
    }

    protected abstract void Excute(WizardPresenter wizard);
}
