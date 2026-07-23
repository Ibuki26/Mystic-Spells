using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioDatabase _database;

    [SerializeField] private AudioSource _bgmSource;

    [SerializeField] private AudioSource _seSource;

    private Dictionary<AudioType, AudioClip> _clips;

    protected override void Awake()
    {
        base.Awake();

        _clips = new Dictionary<AudioType, AudioClip>();

        foreach (var data in _database.AudioList)
        {
            if (!_clips.ContainsKey(data.Type))
            {
                _clips.Add(data.Type, data.Clip);
            }
            else
            {
                Debug.LogWarning($"{data.Type} Ç™èdï°ÇµÇƒÇ¢Ç‹Ç∑");
            }
        }
    }

    public void PlaySE(AudioType type)
    {
        if (_clips.TryGetValue(type, out var clip))
        {
            _seSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"{type} Ç™ìoò^Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    }

    public void PlayBGM(AudioType type)
    {
        if (_clips.TryGetValue(type, out var clip))
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"{type} Ç™ìoò^Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    }
}