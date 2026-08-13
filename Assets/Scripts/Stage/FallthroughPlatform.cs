using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class FallthroughPlatform : MonoBehaviour
{
    private PlatformEffector2D _effector;
    private int _playerLayer;
    private int _defaultLayer;
    private int _oneWayPlatformLayer;
    private bool _isFallingThrough = false;

    private const float FallthroughDuration = 0.5f;

    public void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();

        _playerLayer = LayerMask.NameToLayer("Player");
        _defaultLayer = LayerMask.NameToLayer("Default");
        _oneWayPlatformLayer = LayerMask.NameToLayer("OneWayPlatform");
    }

    public async UniTask Fallthrough()
    {
        if (_isFallingThrough) return;

        _isFallingThrough = true;

        int playerLayerMask = 1 << _playerLayer;

        //Wizard‚ªã‚Éæ‚Á‚Ä‚à‚·‚è”²‚¯‚éÝ’è‚É•ÏX
        _effector.colliderMask &= ~playerLayerMask;
        gameObject.layer = _defaultLayer;

        await UniTask.Delay(TimeSpan.FromSeconds(FallthroughDuration),
            cancellationToken: this.GetCancellationTokenOnDestroy());

        //•ÏX‚ðŒ³‚É–ß‚·
        _effector.colliderMask |= playerLayerMask;
        gameObject.layer = _oneWayPlatformLayer;

        _isFallingThrough = false;
    }
}