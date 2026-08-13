using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _elapsed;
    private float _duration;
    private bool _isRunning = false;

    public bool IsRunning => _isRunning;

    public void StartTimer(float duration)
    {
        _elapsed = 0f;
        _duration = duration;
        _isRunning = true;
    }

    public bool UpdateTimer(float deltaTime)
    {
        if (!_isRunning)
            return false;

        _elapsed += deltaTime;

        if(_elapsed >= _duration)
        {
            _isRunning = false;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _elapsed = 0f;
        _duration = 0f;
        _isRunning = false;
    }
}
