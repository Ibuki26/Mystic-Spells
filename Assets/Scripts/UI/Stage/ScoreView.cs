using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    public void Initialize()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int score)
    {
        _textMesh.SetText("{ 0 }", score);
    }
}
