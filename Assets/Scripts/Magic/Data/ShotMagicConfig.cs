using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMagicConfig : IMagicConfig
{
    private string _magicName;
    private float _coolTime;
    private float _duration;
    private Sprite _icon;
    private GameObject _prefab;
    private Vector3 _spawnOffset;
    private int _power;
    private int _stregnth;
    private float _speed;

    public ShotMagicConfig(ShotMagicConfigData data)
    {
        _magicName = data.MagicName;
        _coolTime = data.CoolTime;
        _duration = data.Duration;
        _icon = data.Icon;
        _prefab = data.Prefab;
        _spawnOffset = data.SpawnOffset;
        _power = data.Power;
        _speed = data.Speed;
    }

    public string MagicName => _magicName;
    public float CoolTime
    {
        get { return _coolTime; }
        set
        {
            if (value < 0f)
                Debug.Log("coolTimeに負の値が代入されています");
            else
                _coolTime = value;
        }
    }
    public float Duration
    {
        get { return _duration; }
        set
        {
            if (value < 0f)
                Debug.Log("durationに負の値が代入されています");
            else
                _duration = value;
        }
    }
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public Vector3 SpawnOffset => _spawnOffset;
    public int Power
    {
        get { return _power; }
        set
        {
            if (value < 0)
                Debug.Log("powerに負の値が代入されています");
            else
                _power = value;
        }
    }
    public float Speed
    {
        get { return _speed; }
        set
        {
            if (value < 0f)
                Debug.Log("speedに負の値が代入されています");
            else
                _speed = value;
        }
    }
}
