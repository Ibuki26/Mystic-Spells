using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private HitPointViewer _hitPointView;
    [SerializeField] private ScorePresenter _score;
    [SerializeField] private RewardLogManager _logManager;

    public void ManualStart()
    {
        _hitPointView.Initialize();
        _score.Initialize();
    }

    public void ResetUI()
    {
        _hitPointView.ResetHitPointViewer();
        _score.ResetScore();
        _logManager.ResetLog();
    }

    public void AddScore(int score)
    {
        _score.AddScore(score);
    }

    public void AddLog(string message, Color color)
    {
        _logManager.AddLog(message, color);
    }
}
