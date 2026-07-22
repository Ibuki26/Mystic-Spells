using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    protected Animator _anim;
    protected int _direction = 0;

    public virtual void Initialize()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetAnimation(string name, bool setBool)
    {
        _anim.SetBool(name, setBool);
    }

    //X•ûŒü‚ÌŒü‚«‚ğİ’è‚·‚é
    public void SetDirectionScale(int direction)
    {
        if (_direction == direction) return;

        var scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x) * direction;
        transform.localScale = new Vector3(scaleX, scale.y, scale.z);
        _direction = direction;
    }
}
