using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class WizardModel
{
    private Subject<int> _hitPointChanged = new Subject<int>();
    public IObservable<int> HitPointChanged => _hitPointChanged;
    
    private CharacterStatus _status;
    private float _speed; //x方向の速度
    private float _jumpVelocity; //y方向の速度
    private int _direction; //向いている方向

    private IMoveSpeedStrategy _moveSpeedStrategy;
    private IDamageStrategy _damageStrategy;
    private IHealStrategy _healStrategy;

    public CharacterStatus Status => _status;

    public WizardModel(WizardModelData data)
    {
        _status = new CharacterStatus(
            data.HitPoint, 
            data.Strength, 
            data.Defense);
        _speed = data.Speed;
        _jumpVelocity = data.JumpVelocity;
        _direction = 1;
    }

    /*
    装備をまだ作っていないため後で
    //装備のステータスをプレイヤーに加算する関数
    public void AddEquipmentStatus(Equipment equipment)
    {
        //装備が無い場合は実行しない
        if (equipment == null) return;

        HitPoint.Value += equipment.hitPoint;
        _strength += equipment.strength;
        _defense += equipment.defense;
        _speed += equipment.speed;
        _maxHitPoint += equipment.hitPoint;
    }
    */

    #region property
    public float Speed
    {
        get { return _speed; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_speedへの代入が負の値です。");
                return;
            }

            _speed = value;
        }
    }

    public float JumpVelocity
    {
        get { return _jumpVelocity; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_jumpへの代入が負の値です。");
                return;
            }

            _jumpVelocity = value;
        }
    }

    public int Direction
    {
        get { return _direction; }
        set
        {
            if (value != 1 && value != -1)
            {
                Debug.Log("_directionへ正しい値が代入されてません。");
                return;
            }

            _direction = value;
        }
    }
#endregion

    public void SetMoveSpeedStrategy(IMoveSpeedStrategy moveSpeedStrategy)
    {
        _moveSpeedStrategy = moveSpeedStrategy;
    }

    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        _damageStrategy = damageStrategy;
    }

    public void SetHealStrategy(IHealStrategy healStrategy)
    {
        _healStrategy = healStrategy;
    }

    //プレイヤーのx方向の速度の計算
    public float CalculateRunSpeed()
    {
        return _moveSpeedStrategy.CalculateMoveSpeed(_speed) * _direction;
    }

    //Model内で数値処理を終わらせるために戻り値をvoidにしたがPresenter側でダメージ数を要求するときがあるかも
    // ダメージ計算を行い、HPを減少させる
    public void CalculateDamage(int attackerStrength, int skillPower)
    {
        int damage = _damageStrategy.CalculateDamage(attackerStrength, skillPower, _status.Defense);
        _status.SetHitPoint(_status.HitPoint - damage);
        _hitPointChanged.OnNext(_status.HitPoint);
    }

    //回復量の計算を行い、HPを増加させる
    public void CalculateHeal(int healPower)
    {
        int healAmount = _healStrategy.CalculateHeal(healPower);
        _status.SetHitPoint(_status.HitPoint + healAmount);
        _hitPointChanged.OnNext(_status.HitPoint);
    }
}
