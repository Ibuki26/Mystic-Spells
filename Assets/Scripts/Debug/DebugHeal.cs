using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHeal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<WizardPresenter>(out var wizard))
        {
            wizard.Model.CalculateHeal(30);
        }
    }
}
