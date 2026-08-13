using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    [SerializeField] private EnemyPresenter[] enemies;

    void Start()
    {
        _wizard.ManualStart();
        UIManager.Instance.ManualStart();
        foreach (var enemy in enemies)
        {
            if(enemy != null)
                enemy.ManualStart();
        }
    }

    void Update()
    {
        _wizard.ManualUpdate();
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.ManualUpdate();
        }
    }

    private void FixedUpdate()
    {
        _wizard.ManualFixedUpdate();
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.ManualFixedUpdate();
        }
    }
}
