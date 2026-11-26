using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    private AreaObjectManager _areaObjectManager;

    void Start()
    {
        _wizard.ManualStart();
        UIManager.Instance.ManualStart();
        _areaObjectManager = AreaObjectManager.Instance;
        _areaObjectManager.EnemyManualStart();
    }

    void Update()
    {
        _wizard.ManualUpdate();
        _areaObjectManager.EnemyManualUpdate();
    }

    private void FixedUpdate()
    {
        _wizard.ManualFixedUpdate();
        _areaObjectManager.EnemyManualFixedUpdate();
    }
}
