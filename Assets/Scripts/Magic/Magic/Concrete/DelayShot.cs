using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class DelayShot : ShotMagic
{
    private Rigidbody2D _rb2d;

    private const float FireDelay = 0.5f;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public override void Initialize(ShotMagicInitContext context)
    {
        base.Initialize(context);

        SetDirectionScale(context.Direction);

        InitializeAsync(context).Forget();
    }

    private async UniTask InitializeAsync(ShotMagicInitContext context)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(FireDelay),
            cancellationToken: this.GetCancellationTokenOnDestroy());

        _rb2d.linearVelocity = new Vector2(context.Speed * context.Direction, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�G�ɓ��������Ƃ��̏���
    }
}
