using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicCreatorStatusData/Shot")]
public class ShotMagicConfigData : ScriptableObject, IMagicConfigData
{
    [SerializeField] private string _magicName;
    [SerializeField] private float _coolTime;
    [SerializeField] private float _duration;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private int _power;
    [SerializeField] private float _speed;

    public string MagicName => _magicName;
    public float CoolTime => _coolTime;
    public float Duration => _duration;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public Vector3 SpawnOffset => _spawnOffset;
    public int Power => _power;
    public float Speed => _speed;
}
