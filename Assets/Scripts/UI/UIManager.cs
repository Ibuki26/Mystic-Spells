using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private HitPointViewer _hitPointView;
    [SerializeField] private ScorePresenter _score;

    public void ManualStart()
    {
        _hitPointView.Initialize();
        _score.Initialize();
    }

    public void AddScore(int score)
    {
        _score.AddScore(score);
    }
}
