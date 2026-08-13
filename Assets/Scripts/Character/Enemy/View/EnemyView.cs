using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

public class EnemyView : CharacterView
{
    [SerializeField] private Color _damagedColor;
    private SpriteRenderer _sr;

    private const float FlashDuration = 0.25f;

    public override void Initialize()
    {
        base.Initialize();
        _sr = GetComponent<SpriteRenderer>();
    }

    //ダメージを受けた演出
    public void FlashDamage()
    {
        DOTween.Sequence()
            .Append(_sr.DOColor(_damagedColor, 0.05f))
            .AppendInterval(FlashDuration)
            .Append(_sr.DOColor(Color.white, 0.05f));
    }
}
