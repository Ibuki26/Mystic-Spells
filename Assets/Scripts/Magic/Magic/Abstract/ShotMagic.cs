using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ボタン入力で即時に発動されるシンプルな魔法クラス。
/// 弾の生成などを行い、長押しやチャージなどの複雑な入力は持たない。
public abstract class ShotMagic : MonoBehaviour, IMagic<ShotMagicInitContext>
{
    protected int _power;
    protected int _strength;
    protected Timer _durationTimer;
    protected bool _isDestoryed = false;
    protected bool _stopAutoDestory = false;

    public int Power => _power;

    public int Strength => _strength;

    public virtual void Initialize(ShotMagicInitContext context)
    {
        _power = context.Power;
        _strength = context.Strength;
        _durationTimer = new Timer();

        _durationTimer.Start(context.Duration);
    }

    private void Update()
    {
        UpdateLifeTime();    
    }

    //継続時間が終了したらオブジェクトを破棄する
    private void UpdateLifeTime()
    {
        if (_durationTimer.IsReady() && !_isDestoryed && !_stopAutoDestory)
        {
            _isDestoryed = true;
            Destroy(gameObject);
        }
    }

    protected void SetDirectionScale(int direction)
    {
        var scale = transform.localScale;

        transform.localScale = new Vector3(
            Mathf.Abs(scale.x) * direction,
            scale.y,
            scale.z
        );
    }
}
