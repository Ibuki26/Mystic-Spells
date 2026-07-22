using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wizard と敵キャラクターが共通で持つステータスを保持するクラス
public class CharacterStatus
{
    private int _hitPoint; //体力
    private int _strength; //攻撃力
    private int _defense; //防御力
    private int _maxHitPoint; //体力の最大値

    public CharacterStatus(int hitPoint, int strength, int defense)
    {
        _hitPoint = hitPoint;
        _strength = strength;
        _defense = defense;
        _maxHitPoint = hitPoint;
    }

    #region property
    public int HitPoint
    {
        get { return _hitPoint; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_hitPointへ負の値が代入されています。");
                return;
            }

            _hitPoint = value;
        }
    }

    public int Strength
    {
        get { return _strength; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_strengthへ負の値が代入されています。");
                return;
            }

            _strength = value;
        }
    }

    public int Defense
    {
        get { return _defense; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_defenseへ負の値が代入されています。");
                return;
            }

            _defense = value;
        }
    }

    public int MaxHitPoint
    {
        get { return _maxHitPoint; }
        set
        {
            if (value < 0)
            {
                Debug.Log("_maxHitPointへの代入が負の値です。");
                return;
            }

            _maxHitPoint = value;
        }
    }
    #endregion

    //HPを有効な範囲に制限して設定する。
    public void SetHitPoint(int value)
    {
        _hitPoint = Mathf.Clamp(value, 0, _maxHitPoint);
    }
}