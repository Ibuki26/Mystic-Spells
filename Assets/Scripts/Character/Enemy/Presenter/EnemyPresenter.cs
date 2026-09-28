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

    private const float DamageCooldownTime = 0.25f;
    private const float DeathLaunchSpeedX = 3f;
    private const float DeathLaunchSpeedY = 10f;
    private const float DeathFallSpeed = -20f;
    private const float DeathFallAcceleration = 20f;
    private const float MinDeathRotation = 10f;
    private const float MaxDeathRotation = 80f;

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
        if (_damageCooldownTimer.UpdateTimer(Time.deltaTime))
            _canTakeDamage = true;
    }

    public virtual void ManualFixedUpdate()
    {
        if(_model.Status.HitPoint == 0)
        {
            UpdateDeathMotion();
            return;
        }
    }

    public void EnterActivationArea()
    {
        _isActivated = true;
    }

    public void ExitActivationArea()
    {
        _isActivated = false;
    }

    public void TakeDamage(DamageContext context)
    {
        //体力が0以下、またはダメージを受けない状態のときは実行しない
        if (_model.Status.HitPoint == 0 || !_canTakeDamage) return;
        
        _canTakeDamage = false;
        _model.TakeDamage(context);
        
        //ダメージ量を画面に表示
        
        //体力が0なら死亡
        if (_model.Status.HitPoint == 0)
        {
            Die(context.Direction);
            return;
        }
            
        AudioManager.Instance.PlaySE(AudioType.EnemyDamage);
        _view.FlashDamage();
        
        _damageCooldownTimer.StartTimer(DamageCooldownTime);
    }

    private void Die(int direction)
    {
        _isActivated = false;

        UIManager.Instance.AddScore(_model.Score);

        AudioManager.Instance.PlaySE(AudioType.EnemyDie);

        GetComponent<Collider2D>().enabled = false;

        _rb2d.linearVelocity = new Vector2(DeathLaunchSpeedX * direction, DeathLaunchSpeedY);

        var randomRotate = Random.Range(MinDeathRotation, MaxDeathRotation);
        _rb2d.DORotate(randomRotate * _model.Direction, 0.5f);
    }

    //死亡時の落下演出
    private void UpdateDeathMotion()
    {
        var velocityY = Mathf.MoveTowards(_rb2d.linearVelocityY, DeathFallSpeed, DeathFallAcceleration * Time.fixedDeltaTime);

        _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocityX, velocityY);
    }

    private void OnBecameInvisible()
    {
        if (_model.Status.HitPoint > 0)
            return;
        
        //死亡後画面外に出たら破棄される
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var parent = collision.transform.parent;
        if (parent == null) return;

        if(parent.TryGetComponent<WizardPresenter>(out var wizard))
        {
            var context = new DamageContext(_model.Status.Strength, _model.Power, _model.Direction, DamageType.Normal);
            wizard.TakeDamage(context);
        }

        if(parent.TryGetComponent<WizardAgent>(out var agent))
        {
            Debug.Log("touch enemy");
            agent.AddReward(-0.01f);
        }
    }
}
