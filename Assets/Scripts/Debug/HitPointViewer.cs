using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class HitPointViewer : MonoBehaviour
{
    [SerializeField] private WizardPresenter _wizard;
    private TextMeshProUGUI _textMesh;

    public void Initialize()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.SetText(_wizard.Model.Status.HitPoint.ToString());
        Bind();
    }

    private void Bind()
    {
        _wizard.Model.HitPointChanged
            .Subscribe(hp =>
            {
                _textMesh.SetText(hp.ToString());
            })
            .AddTo(gameObject);
    }

    public void ResetHitPointViewer()
    {
        _textMesh.SetText(200.ToString());
    }
}
