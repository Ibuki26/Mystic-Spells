using UnityEngine;

public class GoalFlag : MonoBehaviour
{
    private bool _isGoalReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isGoalReached) return;

        if(collision.TryGetComponent<WizardPresenter>(out var wizard))
        {
            _isGoalReached = true;
            //ƒS[ƒ‹ˆ—
        }
    }
}
