using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    private ScoreModel _model;
    private ScoreView _view;

    public void Initialize()
    {
        _model = new ScoreModel();
        _view = GetComponent<ScoreView>();
        _view.Initialize();
    }

    public void AddScore(int score)
    {
        _model.Score += score;
        _view.UpdateText(_model.Score);
    }
}
