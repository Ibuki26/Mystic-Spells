using UnityEngine;

public class ScoreModel
{
    private int _score;

    public int Score
    {
        get { return _score; }
        set
        {
            if(value < 0)
            {
                Debug.Log("_score‚Ö‚Ì‘ã“ü‚ª•‰‚Ì’l‚Å‚·B");
                return;
            }

            _score = value;
        }
    }
}
