using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private HitPointViewer _hitPointView;

    public void ManualStart()
    {
        _hitPointView.Initialize();
    }
}
