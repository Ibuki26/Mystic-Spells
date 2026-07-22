using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyPresenter : MonoBehaviour, IActivationAreaReceiver
{
    [SerializeField] protected int hitPoint;
    [SerializeField] protected int strength;
    [SerializeField] protected int defense;
    [SerializeField] protected int score;
    [SerializeField] protected int power;
    [SerializeField] protected int direction;

    protected EnemyModel _model;
    protected EnemyView _view;
    protected Rigidbody2D _rb2d;
    protected Timer _damageCooldownTimer;

    protected bool _isActivated = false;
    protected bool _canTakeDamage = true;

    private const float DamageCooldownTime = 0.5f;
    private const float DeathFallSpeed = -4f;

    public virtual void ManualStart()
    {
        _model = new EnemyModel(hitPoint, strength, defense, score, power, direction);
        _view = GetComponent<EnemyView>();
        _rb2d = GetComponent<Rigidbody2D>();
        _damageCooldownTimer = new Timer();

        _model.SetDamageStrategy(new NormalDamage());
        _view.Initialize();
    }

    public virtual void ManualUpdate()
    {
        if (_damageCooldownTimer.IsReady())
            _canTakeDamage = true;
    }

    public abstract void ManualFixedUpdate();

    public void EnterActivationArea()
    {
        _isActivated = true;
        Debug.Log("enter");
    }

    public void ExitActivationArea()
    {
        _isActivated = false;
        Debug.Log("exit");
    }

    public void TakeDamage(int strength, int power)
    {
        //体力が0以下、またはダメージを受けない状態のときは実行しない
        if (_model.Status.HitPoint <= 0 || !_canTakeDamage) return;

        _canTakeDamage = false;
        
        //音声を流す

        _view.FlashDamage();

        _model.CalculateDamage(strength, power);

        //ダメージ量を画面に表示

        _damageCooldownTimer.Start(DamageCooldownTime);

        //体力が0なら死亡
        if (_model.Status.HitPoint == 0)
            Die();
    }

    private void Die()
    {
        _isActivated = false;

        //スコアの加算

        //SEを流す

        GetComponent<Collider2D>().enabled = false;

        _rb2d.linearVelocity = new Vector2(0, DeathFallSpeed);

        _rb2d.DORotate( -90 * _model.Direction, 1)
            .OnComplete(() => Destroy(gameObject));

        Debug.Log(transform.name + " Die");
    }
}
