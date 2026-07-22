using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ShotMagic
{
    private Rigidbody2D _rb2d;
    private Animator _anim;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    public override void Initialize(ShotMagicInitContext context)
    {
        base.Initialize(context);

        _rb2d.linearVelocity = new Vector2(context.Speed * context.Direction, 0);

        SetDirectionScale(context.Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�G�L�����N�^�[�ɓ���������폜
    }
}
