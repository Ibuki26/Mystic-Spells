using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    private CharacterStatus _status;
    private int _score;
    private int _power;
    private int _direction;

    private IDamageStrategy _damageStrategy;
    private IHealStrategy _healStrategy;

    public CharacterStatus Status => _status;

    public EnemyModel(int hitPoint, int strength, int defense, int score, int power, int direction)
    {
        _status = new CharacterStatus(
            hitPoint,
            strength,
            defense);
        _score = score;
        _power = power;
        _direction = direction;
    }

    #region property
    public int Score
    {
        get { return _score; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_scoreへ負の値が代入されています。");
                return;
            }

            _score = value;
        }
    }

    public int Power
    {
        get { return _power; }
        set
        {
            if(value < 0)
            {
                Debug.Log("_powerへ負の値が代入されています。");
                return;
            }

            _power = value;
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

    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        _damageStrategy = damageStrategy;
    }

    public void SetHealStrategy(IHealStrategy healStrategy)
    {
        _healStrategy = healStrategy;
    }

    // ダメージ計算を行い、HPを減少させる
    public void CalculateDamage(int attackerStrength, int skillPower)
    {
        int damage = _damageStrategy.CalculateDamage(attackerStrength, skillPower, _status.Defense);
        _status.SetHitPoint(_status.HitPoint - damage);
    }

    //回復量の計算を行い、HPを増加させる
    public void CalculateHeal(int healPower)
    {
        int healAmount = _healStrategy.CalculateHeal(healPower);
        _status.SetHitPoint(_status.HitPoint + healAmount);
    }
}
