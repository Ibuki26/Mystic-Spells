using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidShot : ShotMagic
{
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public override void Initialize(ShotMagicInitContext context)
    {
        base.Initialize(context);
        AudioManager.Instance.PlaySE(AudioType.RapidShot);
        _rb2d.linearVelocity = new Vector2(context.Speed * context.Direction, 0);

        SetDirectionScale(context.Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<EnemyPresenter>(out var enemy))
        {
            enemy.TakeDamage(_strength, _power);
        }
    }
}
