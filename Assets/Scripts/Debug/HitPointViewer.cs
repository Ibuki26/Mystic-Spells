using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class HitPointViewer : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    private TextMeshProUGUI _textMesh;

    public void ManualStart()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = _wizard.Model.Status.HitPoint.ToString();
        Bind();
    }

    private void Bind()
    {
        _wizard.Model.HitPointChanged
            .Subscribe(hp =>
            {
                _textMesh.text = hp.ToString();
            })
            .AddTo(gameObject);
    }
}
