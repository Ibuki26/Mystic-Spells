using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowWizardStateFlags : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    [SerializeField] private WizardStateFlags _wantToShowState;
    private TextMeshProUGUI _textMesh;

    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var checkBool = (_wizard.StateFlags & _wantToShowState) != 0;
        var text = checkBool ? "Åõ" : "Å~";
        _textMesh.SetText(_wantToShowState + " : " + text);
    }
}
