using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardView : CharacterView
{
    public void SetAnimationTrigger(string name)
    {
        _anim.SetTrigger(name);
    }
}
