using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    [SerializeField] private AreaObjectManager areaObjectManager;

    void Start()
    {
        _wizard.ManualStart();
        UIManager.Instance.ManualStart();
        areaObjectManager.EnemyManualStart();
    }

    void Update()
    {
        _wizard.ManualUpdate();
        areaObjectManager.EnemyManualUpdate();
    }

    private void FixedUpdate()
    {
        _wizard.ManualFixedUpdate();
        areaObjectManager.EnemyManualFixedUpdate();
    }
}
