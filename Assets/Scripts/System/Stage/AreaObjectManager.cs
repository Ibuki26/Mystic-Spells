using UnityEngine;

public class AreaObjectManager : MonoBehaviour
{
    [SerializeField] AreaObjectGroup[] areaObjectGroups;
    [SerializeField] GoalFlag goalFlag;

    public void ResetGroup()
    {
        if (areaObjectGroups != null)
        {
            foreach (var group in areaObjectGroups)
                group.ResetObjects();
        }

        if(goalFlag != null)
            goalFlag.ResetGimmick();
    }
}
