using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _lastUseTime = -Mathf.Infinity;
    private float _duration;

    public bool IsReady()
    {
        return Time.time >= _lastUseTime + _duration;
    }

    public void Start(float duration)
    {
        _duration = duration;
        _lastUseTime = Time.time;
    }
}
