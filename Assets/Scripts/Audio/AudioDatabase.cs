using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [SerializeField]
    private List<AudioData> _audioList = new();

    public IReadOnlyList<AudioData> AudioList => _audioList;

#if UNITY_EDITOR
    public void SetAudioList(List<AudioData> list)
    {
        _audioList = list;
    }
#endif
}