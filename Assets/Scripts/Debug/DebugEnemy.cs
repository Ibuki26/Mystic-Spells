using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<WizardPresenter>(out var wizard))
        {
            wizard.Model.CalculateDamage(50, 20);
        }
    }
}
