using UnityEngine;

public class GoalFlag : MonoBehaviour
{
    private bool _isGoalReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isGoalReached) return;

        if(collision.TryGetComponent<WizardAgent>(out var agent))
        {
            _isGoalReached = true;
            //ƒS[ƒ‹ˆ—
            Debug.Log("goal");
            agent.AddReward(1.0f);
            agent.EndEpisode();
        }
    }
}
